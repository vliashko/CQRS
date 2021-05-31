using CQRS.Application.Configuration.DomainEvents;
using CQRS.Domain.Customers;
using CQRS.Domain.Customers.Orders;
using CQRS.Domain.Customers.Orders.Events;
using Newtonsoft.Json;

namespace CQRS.Application.Orders.PlaceCustomerOrder
{
    public class OrderPlacedNotification : DomainNotificationBase<OrderPlacedEvent>
    {
        public OrderId OrderId { get; }
        public CustomerId CustomerId { get; }

        public OrderPlacedNotification(OrderPlacedEvent domainEvent) : base(domainEvent)
        {
            OrderId = domainEvent.OrderId;
            CustomerId = domainEvent.CustomerId;
        }

        [JsonConstructor]
        public OrderPlacedNotification(OrderId orderId, CustomerId customerId) : base(null)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}