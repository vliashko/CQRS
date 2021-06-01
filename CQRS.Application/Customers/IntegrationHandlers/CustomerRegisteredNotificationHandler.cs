using CQRS.Application.Configuration.Processing;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.Customers.IntegrationHandlers
{
    public class CustomerRegisteredNotificationHandler : INotificationHandler<CustomerRegisteredNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public CustomerRegisteredNotificationHandler(
            ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(CustomerRegisteredNotification notification, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(new MarkCustomerAsWelcomedCommand(
                Guid.NewGuid(),
                notification.CustomerId));
        }
    }
}