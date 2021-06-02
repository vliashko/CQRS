namespace CQRS.Application.Payments.PaymentSystem
{
    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public string ClientToken { get; set; }
        public string CardNumber { get; set; }
        public string Date { get; set; }
    }
}
