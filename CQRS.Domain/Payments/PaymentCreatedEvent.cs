using CQRS.Domain.Customers.Orders;
using CQRS.Domain.SeedWork;

namespace CQRS.Domain.Payments
{
    public class PaymentCreatedEvent : DomainEventBase
    {
        public PaymentCreatedEvent(PaymentId paymentId, OrderId orderId)
        {
            PaymentId = paymentId;
            OrderId = orderId;
        }

        public PaymentId PaymentId { get; }

        public OrderId OrderId { get; }
    }
}