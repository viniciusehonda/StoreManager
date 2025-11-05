using SharedKernel;

namespace InternalUserService.Domain;

public sealed record UserRegisteredDomainEvent(Guid UserId, string email, string firstName, string lastName) : IDomainEvent;
