using CQRS.Application.Configuration.DomainEvents;
using CQRS.Domain.Payments;
using Newtonsoft.Json;

namespace CQRS.Application.Payments
{
    public class PaymentCreatedNotification : DomainNotificationBase<PaymentCreatedEvent>
    {
        public PaymentId PaymentId { get; }

        public PaymentCreatedNotification(PaymentCreatedEvent domainEvent) : base(domainEvent)
        {
            PaymentId = domainEvent.PaymentId;
        }

        [JsonConstructor]
        public PaymentCreatedNotification(PaymentId paymentId) : base(null)
        {
            PaymentId = paymentId;
        }
    }
}