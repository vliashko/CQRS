using CQRS.Domain.Products;

namespace CQRS.Domain.Customers.Orders
{
    public class OrderProductData
    {
        public OrderProductData(ProductId productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public ProductId ProductId { get; }

        public int Quantity { get; }
    }
}