using CQRS.Domain.SeedWork;
using System;

namespace CQRS.Domain.Customers.Orders
{
    public class OrderId : TypedIdValueBase
    {
        public OrderId(Guid value) : base(value)
        {
        }
    }
}