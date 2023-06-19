using Hahn.Customers.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Hahn.Customers.Api.DI
{
    public static class SqliteConfiguration
    {
        public static IServiceCollection AddSqliteConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomerContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlite(connectionString,
                     sqlOptions =>
                    {
                        //  string ddd = typeof(Program).GetType().Assembly.GetName().Name;
                        sqlOptions.MigrationsAssembly("Hahn.Customers.Infrastructure");
                    });
            },
            ServiceLifetime.Scoped);
            return services;
        }

        public static void MigrateDbBeforeRunningApp(WebApplication app)
        {
            int i = 0;
            while (i++ < 3)
            {
                try
                {
                    using (var scope = app.Services.CreateScope())
                    {
                        var services = scope.ServiceProvider;

                        var context = services.GetRequiredService<CustomerContext>();
                        if (context.Database.GetPendingMigrations().Any())
                        {
                            context.Database.Migrate();
                        }
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error in checking for db pending migrations");
                    Console.WriteLine(ex.Message);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
