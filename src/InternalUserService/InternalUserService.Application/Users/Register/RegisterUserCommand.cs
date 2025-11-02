using InternalUserService.Application.Abstractions.Messaging;

namespace InternalUserService.Application.Users.Register;

public sealed record RegisterUserCommand(string Email, string FirstName, string LastName, string Password)
    : ICommand<Guid>;
