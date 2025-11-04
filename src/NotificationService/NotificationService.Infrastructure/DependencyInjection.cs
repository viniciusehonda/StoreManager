using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Domain.Interfaces;
using NotificationService.Infrastructure.Notifications;

namespace NotificationService.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddServices()
            .AddHealthChecks(configuration);

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationSender, EmailSender>();
        services.AddHostedService<KafkaConsumerService>();

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        string? boostrapServers = configuration["Kafka:BootstrapServers"];

        services
            .AddHealthChecks()
            .AddKafka(new ProducerConfig
            {
                BootstrapServers = boostrapServers
            },
            name: "kafka-broker-check",
            failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded,
            tags: ["ready", "kafka"]);

        return services;
    }
}
