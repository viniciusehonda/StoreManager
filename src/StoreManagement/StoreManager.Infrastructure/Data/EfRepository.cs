using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using StoreManager.SharedKernel;

namespace StoreManagement.Infrastructure.Data;
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}