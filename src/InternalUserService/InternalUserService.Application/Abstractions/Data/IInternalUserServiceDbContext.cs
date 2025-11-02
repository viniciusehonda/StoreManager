using InternalUserService.Domain;
using Microsoft.EntityFrameworkCore;

namespace InternalUserService.Application.Abstractions.Data;

public interface IInternalUserServiceDbContext
{
    DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
