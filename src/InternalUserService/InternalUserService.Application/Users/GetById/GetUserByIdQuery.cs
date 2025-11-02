using InternalUserService.Application.Abstractions.Messaging;

namespace InternalUserService.Application.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
