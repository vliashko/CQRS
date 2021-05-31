using CQRS.Domain.SeedWork;

namespace CQRS.Domain.Products
{
    public class Product : Entity, IAggregateRoot
    {
        public ProductId Id { get; private set; }

        public string Name { get; private set; }

        private Product()
        {

        }
    }
}