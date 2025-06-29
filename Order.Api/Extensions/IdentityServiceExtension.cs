using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Order.Data.Context;
using Order.Data.Entities.IdentityEntities;
using System.Text;

namespace Order.API.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration _configuration)
        {
            var builder = services.AddIdentity<AppUser, UserRoles>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();
            //var builder = services.AddIdentityCore<AppUser>();

            builder = new IdentityBuilder(builder.UserType, builder.Services);

            //builder.AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["Token:Issuer"],
                        ValidAudience = _configuration["Token:Issuer"],
                        ValidateAudience = true,
                        ValidateLifetime = true
                    };
                });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            //    options.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"));
            //});

            return services;
        }
    }
}
