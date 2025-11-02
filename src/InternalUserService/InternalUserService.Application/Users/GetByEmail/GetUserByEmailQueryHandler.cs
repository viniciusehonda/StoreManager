using InternalUserService.Application.Abstractions.Authentication;
using InternalUserService.Application.Abstractions.Data;
using InternalUserService.Application.Abstractions.Messaging;
using InternalUserService.Domain;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace InternalUserService.Application.Users.GetByEmail;

internal sealed class GetUserByEmailQueryHandler(IInternalUserServiceDbContext context, IUserContext userContext)
    : IQueryHandler<GetUserByEmailQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        UserResponse? user = await context.Users
            .Where(u => u.Email == query.Email)
            .Select(u => new UserResponse
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFoundByEmail);
        }

        if (user.Id != userContext.UserId)
        {
            return Result.Failure<UserResponse>(UserErrors.Unauthorized());
        }

        return user;
    }
}
