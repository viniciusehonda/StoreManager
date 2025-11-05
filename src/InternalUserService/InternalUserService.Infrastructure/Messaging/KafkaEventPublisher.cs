using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using SharedKernel;

namespace InternalUserService.Infrastructure.Messaging;
public class KafkaEventPublisher : IEventPublisher
{
    private readonly IProducer<Null, string> _producer;

    public KafkaEventPublisher(IConfiguration config)
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = config["Kafka:BootstrapServers"]
        };

        _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    }

    public async Task PublishAsync<T>(T @event, string topic)
    {
        var message = new Message<Null, string>
        {
            Value = JsonSerializer.Serialize(@event)
        };

        await _producer.ProduceAsync(topic, message);
    }
}
