using Autofac;
using CQRS.Application.Customers.DomainServices;
using CQRS.Domain.Customers;
using CQRS.Domain.ForeignExchange;
using CQRS.Infrastructure.Domain.ForeignExchanges;

namespace CQRS.Infrastructure.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerUniquenessChecker>()
                .As<ICustomerUniquenessChecker>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ForeignExchange>()
                .As<IForeignExchange>()
                .InstancePerLifetimeScope();
        }
    }
}