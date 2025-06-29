using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Order.Data.Entities.IdentityEntities;


namespace Order.Repository
{
    public static class AppIdentityContextSeed
    {
        public static async Task SeedRolesAsync(RoleManager<UserRoles> roleManager, ILoggerFactory logger)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new[]
                {
                    new UserRoles
                    {
                        Name = "Admin",
                        ConcurrencyStamp = "Admin",
                        CreatedAt = DateTime.Now,
                        NormalizedName = "ADMIN"
                    },
                    new UserRoles
                    {
                        Name = "Customer",
                        ConcurrencyStamp = "Customer",
                        CreatedAt = DateTime.Now,
                        NormalizedName = "CUSTOMER"
                    }
                };

                foreach (var role in roles)
                {
                    var result = await roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                    {
                        var loggerFectory = logger.CreateLogger($"Failed to create role {role.Name}");
                    }
                }
            }
        }


        public static async Task SeedAdminAsync(UserManager<AppUser> userManager)
        {
            var user = new AppUser
            {
                DisplayName = "Kareem Waleed",
                Email = "kareemwaleed654@gmail.com",
                NormalizedEmail = "kareemwaleed654@gmail.com".ToUpper(),
                UserName = "kareemwaleed654",
                NormalizedUserName = "kareemwaleed654".ToUpper()
            };

            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                await userManager.CreateAsync(user, "#Adminuser@12345");
            }

            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
