using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hahn.Customers.Api.DI;
using Hahn.Customers.Infrastructure.FluentValidations.Validations;
using Hahn.Customers.Infrastructure.FluentValidations;
using MediatR;
using FluentValidation;
using Hahn.Customers.Api.Infrustructure.Filters;

namespace Hahn.Customers.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new MediatRModule());
                builder.RegisterModule(new ApplicationModule(connectionString));
            });

            builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerCommandValidator>();
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

            builder.Services.AddSqliteConfiguration(builder.Configuration);
            builder.Services.AddCustomSwagger(builder.Configuration);
            builder.Services.AddControllers(config =>
            {
                config.Filters.Add(typeof(HttpGlobalExceptionFilter));
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            SqliteConfiguration.MigrateDbBeforeRunningApp(app);//useful for docker container.
            app.Run();
        }
    }
}