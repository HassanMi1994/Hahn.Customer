using Microsoft.OpenApi.Models;

namespace Hahn.Customers.Api.DI
{
    public static class SwaggerConfiguratoin
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Customer - HTTP API",
                    Version = "v1",
                    Description = "The Customer Service HTTP API"
                });
            });

            return services;
        }
    }
}
