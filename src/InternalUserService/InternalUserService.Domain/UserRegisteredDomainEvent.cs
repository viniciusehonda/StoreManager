using SharedKernel;

namespace InternalUserService.Domain;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;
