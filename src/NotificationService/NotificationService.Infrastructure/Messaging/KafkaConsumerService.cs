using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotificationService.Application;
using NotificationService.Application.DTOs;

namespace NotificationService.Infrastructure;
public class KafkaConsumerService : BackgroundService
{
    private readonly IConfiguration _config;
    private readonly ILogger<KafkaConsumerService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public KafkaConsumerService(
        IConfiguration config,
        ILogger<KafkaConsumerService> logger,
        IServiceProvider serviceProvider)
    {
        _config = config;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var conf = new ConsumerConfig
        {
            BootstrapServers = _config["Kafka:BootstrapServers"],
            GroupId = _config["Kafka:GroupId"],
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using IConsumer<Ignore, string> consumer = new ConsumerBuilder<Ignore, string>(conf).Build();
        consumer.Subscribe(_config["Kafka:Topic"]);

        _logger.LogInformation("Consumer Kafka iniciado...");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                ConsumeResult<Ignore, string> cr = consumer.Consume(stoppingToken);
                NotificationDto? dto = JsonSerializer.Deserialize<NotificationDto>(cr.Message.Value);

                if (dto == null)
                {
                    continue;
                }

                using IServiceScope scope = _serviceProvider.CreateScope();
                NotificationAppService appService = scope.ServiceProvider.GetRequiredService<NotificationAppService>();
                await appService.HandleNotificationAsync(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar mensagem Kafka.");
            }
        }
    }
}
