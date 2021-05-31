using CQRS.Domain.SharedKernel;

namespace CQRS.Domain.Products
{
    public class ProductPrice
    {
        public MoneyValue Value { get; private set; }

        private ProductPrice()
        {

        }
    }
}