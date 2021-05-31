using CQRS.Application.Configuration.Commands;
using CQRS.Domain.Payments;
using MediatR;
using Newtonsoft.Json;
using System;

namespace CQRS.Application.Payments.SendEmailAfterPayment
{
    public class SendEmailAfterPaymentCommand : InternalCommandBase<Unit>
    {
        public PaymentId PaymentId { get; }

        [JsonConstructor]
        public SendEmailAfterPaymentCommand(Guid id, PaymentId paymentId) : base(id)
        {
            PaymentId = paymentId;
        }
    }
}