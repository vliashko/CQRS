using CQRS.Domain.SeedWork;
using CQRS.Infrastructure.Database;
using CQRS.Infrastructure.Processing;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrdersContext _ordersContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(
            OrdersContext ordersContext,
            IDomainEventsDispatcher domainEventsDispatcher)
        {
            _ordersContext = ordersContext;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            await _domainEventsDispatcher.DispatchEventsAsync();
            return await _ordersContext.SaveChangesAsync(cancellationToken);
        }
    }
}