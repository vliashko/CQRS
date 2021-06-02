namespace CQRS.Application.Payments.PaymentSystem
{
    public class ProductDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
    }
}
