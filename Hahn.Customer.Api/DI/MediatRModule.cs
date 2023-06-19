using Autofac;
using Hahn.Customers.Infrastructure.CQRS.Commands;
using MediatR;
using System.Reflection;

namespace Hahn.Customers.Api.DI
{
    public class MediatRModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(CreateCustomerCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterAssemblyTypes(typeof(CreateCustomerCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

        }
    }
}
