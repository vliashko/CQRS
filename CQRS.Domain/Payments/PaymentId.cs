using CQRS.Domain.SeedWork;
using System;

namespace CQRS.Domain.Payments
{
    public class PaymentId : TypedIdValueBase
    {
        public PaymentId(Guid value) : base(value)
        {
        }
    }
}