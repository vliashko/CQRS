using CQRS.Domain.SeedWork;
using System.Collections.Generic;

namespace CQRS.Domain.Products
{
    public class Product : Entity, IAggregateRoot
    {
        public ProductId Id { get; private set; }

        public string Name { get; private set; }

        private List<ProductPrice> _prices;

        private Product()
        {

        }
    }
}