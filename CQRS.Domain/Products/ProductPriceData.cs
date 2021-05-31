using CQRS.Domain.SeedWork;
using CQRS.Domain.SharedKernel;

namespace CQRS.Domain.Products
{
    public class ProductPriceData : ValueObject
    {
        public ProductPriceData(ProductId productId, MoneyValue price)
        {
            ProductId = productId;
            Price = price;
        }

        public ProductId ProductId { get; }

        public MoneyValue Price { get; }
    }
}