using InternalUserService.Domain;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace InternalUserService.Application.Users.Register;

internal sealed class UserRegisteredDomainEventHandler : IDomainEventHandler<UserRegisteredDomainEvent>
{
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<UserRegisteredDomainEventHandler> _logger;

    public UserRegisteredDomainEventHandler(IEventPublisher eventPublisher,
            ILogger<UserRegisteredDomainEventHandler> logger)
    {
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public async Task Handle(UserRegisteredDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UserRegisteredEvent for {UserId}", domainEvent.UserId);

        await _eventPublisher.PublishAsync(domainEvent, topic: "user-events");

        _logger.LogInformation("UserRegisteredEvent published to Kafka topic 'user-events'");
    }
}
