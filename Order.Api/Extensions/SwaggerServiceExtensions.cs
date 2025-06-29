using Microsoft.OpenApi.Models;

namespace Order.API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderManagementSystemApi", Version = "v1" });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization Header Using The Bearer Schema. Example: \"Authorization: Bearer{token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearer"
                    }
                };

                options.AddSecurityDefinition("bearer", securityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme,new[] { "bearer" } }
                });
            });

            return services;
        }
    }
}
