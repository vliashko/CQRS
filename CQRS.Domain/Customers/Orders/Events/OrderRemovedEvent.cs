using CQRS.Domain.SeedWork;

namespace CQRS.Domain.Customers.Orders.Events
{
    public class OrderRemovedEvent : DomainEventBase
    {
        public OrderId OrderId { get; }

        public OrderRemovedEvent(OrderId orderId)
        {
            OrderId = orderId;
        }
    }
}