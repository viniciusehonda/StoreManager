using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreManagement.Core;
using StoreManagement.Infrastructure.Data;

namespace StoreManagement.Infrastructure.Data;
public static class SeedData
{
    public static readonly User User1 = new("John", "Doe", "john_doe@email.com");
    public static readonly User User2 = new("Average", "Joe", "average_joe@email.com");

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var dbContext = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
        {
            if (dbContext.Users.Any()) return;   // DB has been seeded
            PopulateTestData(dbContext);
        }
    }
    public static void PopulateTestData(AppDbContext dbContext)
    {
        foreach (var user in dbContext.Users)
        {
            dbContext.Remove(user);
        }

        dbContext.SaveChanges();

        dbContext.Users.Add(User1);
        dbContext.Users.Add(User2);

        dbContext.SaveChanges();
    }
}
