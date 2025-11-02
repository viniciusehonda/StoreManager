using InternalUserService.Application.Abstractions.Authentication;
using InternalUserService.Application.Abstractions.Data;
using InternalUserService.Application.Abstractions.Messaging;
using InternalUserService.Domain;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace InternalUserService.Application.Users.Register;

internal sealed class RegisterUserCommandHandler(IInternalUserServiceDbContext context, IPasswordHasher passwordHasher)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
            PasswordHash = passwordHasher.Hash(command.Password)
        };

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        context.Users.Add(user);

        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
