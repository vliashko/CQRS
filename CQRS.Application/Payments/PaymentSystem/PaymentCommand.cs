using CQRS.Application.Configuration.Commands;
using CQRS.Domain.Payments;
using MediatR;

namespace CQRS.Application.Payments.PaymentSystem
{
    public class PaymentCommand : CommandBase<Unit>
    {
        public PaymentId PaymentId { get; }
        public PaymentRequest PaymentRequest { get; }

        public PaymentCommand(
            PaymentId paymentId,
            PaymentRequest payment)
        {
            PaymentId = paymentId;
            PaymentRequest = payment;
        }
    }
}
