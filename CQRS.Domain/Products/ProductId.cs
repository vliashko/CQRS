using CQRS.Domain.SeedWork;
using System;

namespace CQRS.Domain.Products
{
    public class ProductId : TypedIdValueBase
    {
        public ProductId(Guid value) : base(value)
        {
        }
    }
}