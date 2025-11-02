using InternalUserService.Application.Abstractions.Messaging;

namespace InternalUserService.Application.Users.GetByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;
