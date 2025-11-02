using InternalUserService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace InternalUserService.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using InternalUserServiceDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<InternalUserServiceDbContext>();

        dbContext.Database.Migrate();
    }
}
