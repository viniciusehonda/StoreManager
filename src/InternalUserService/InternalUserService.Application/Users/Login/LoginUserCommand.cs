using InternalUserService.Application.Abstractions.Messaging;

namespace InternalUserService.Application.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<string>;
