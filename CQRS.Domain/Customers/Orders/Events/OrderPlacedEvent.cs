using CQRS.Domain.SeedWork;
using CQRS.Domain.SharedKernel;

namespace CQRS.Domain.Customers.Orders.Events
{
    public class OrderPlacedEvent : DomainEventBase
    {
        public OrderId OrderId { get; }

        public CustomerId CustomerId { get; }

        public MoneyValue Value { get; }

        public OrderPlacedEvent(
            OrderId orderId,
            CustomerId customerId,
            MoneyValue value)
        {
            OrderId = orderId;
            CustomerId = customerId;
            Value = value;
        }
    }
}