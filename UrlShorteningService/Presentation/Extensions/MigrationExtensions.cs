using Microsoft.EntityFrameworkCore;
using ShorteningService.Infrastructure.EFCore;

namespace ShorteningService.Presentation.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                context.Database.EnsureDeleted();

                if (context.Database.GetPendingMigrations().Count() > 0)
                {
                    context.Database.Migrate();
                }

                SeedData(context);
            }
        }

        private static void SeedData(AppDbContext context)
        {


        }

    }
}
