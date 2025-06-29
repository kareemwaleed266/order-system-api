using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Order.Data.Context;
using Order.Data.Entities.IdentityEntities;
using Order.Repository;

namespace Order.API.Helper
{
    public class ApplySeeding
    {
        public static async Task ApplySeedingAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<AppIdentityDbContext>();
                    var roleManager = services.GetRequiredService<RoleManager<UserRoles>>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    await context.Database.MigrateAsync();
                    await AppIdentityContextSeed.SeedRolesAsync(roleManager, loggerFactory);
                    await AppIdentityContextSeed.SeedAdminAsync(userManager);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<ApplySeeding>();
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}
