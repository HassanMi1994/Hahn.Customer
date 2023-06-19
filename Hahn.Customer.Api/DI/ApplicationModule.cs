using Autofac;
using Hahn.Customers.Domain.Aggregates;
using Hahn.Customers.Infrastructure.Repositories;

namespace Hahn.Customers.Api.DI
{

    public class ApplicationModule : Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string connectinoString)
        {
            QueriesConnectionString = connectinoString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerRepository>()
                .As<ICustomerRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
