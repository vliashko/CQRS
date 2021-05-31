using CQRS.Domain.SeedWork;
using System;

namespace CQRS.Domain.Customers
{
    public class CustomerId : TypedIdValueBase
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}