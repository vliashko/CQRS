using CQRS.Domain.Customers.Orders;

namespace CQRS.Domain.Payments
{
    public static class PaymentNotificationService
    {
        public static string GetPaymentEmailConfirmationDescription(PaymentId paymentId, OrderId orderId)
        {
            return $"Payment with number: {paymentId.Value} was created for order with number: {orderId.Value}";
        }
    }
}
