using GoCode.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GoCode.Infrastructure.Extensions
{
    public static class SeedDatabaseExtension
    {
        public static async Task SeedDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<ApplicationDbSeeder>();
            await dbInitializer.Seed();
        }
    }
}
