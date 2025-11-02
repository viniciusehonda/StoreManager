using InternalUserService.Application.Abstractions.Authentication;
using InternalUserService.Application.Abstractions.Data;
using InternalUserService.Application.Abstractions.Messaging;
using InternalUserService.Domain;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace InternalUserService.Application.Users.Login;

internal sealed class LoginUserCommandHandler(
    IInternalUserServiceDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : ICommandHandler<LoginUserCommand, string>
{
    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        string token = tokenProvider.Create(user);

        return token;
    }
}
