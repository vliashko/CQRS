using Braintree;
using CQRS.Application.Configuration.Commands;
using CQRS.Application.Configuration.Data;
using CQRS.Application.Configuration.Emails;
using CQRS.Domain.Payments;
using CQRS.Domain.Payments.Braintree;
using Dapper;
using MediatR;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.Payments.PaymentSystem
{
    public class PaymentCommandHandler : ICommandHandler<PaymentCommand, Unit>
    {
        private readonly BraintreeConfiguration _config = new BraintreeConfiguration();
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IEmailSender _emailSender;

        public PaymentCommandHandler(ISqlConnectionFactory sqlConnectionFactory,
            IPaymentRepository paymentRepository,
            IEmailSender emailSender)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _paymentRepository = paymentRepository;
            _emailSender = emailSender;
        }

        public async Task<Unit> Handle(PaymentCommand request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);

            var gateway = _config.GetGateway();

            var sql = "SELECT [Order].[Value]" +
                      "FROM orders.Orders AS [Order], payments.Payments AS [Payment]" +
                      "WHERE [Payment].[Id] = @Id AND [Payment].[OrderId] = [Order].[Id]";
            var amount = await connection.QueryFirstAsync<decimal>(sql,
            new
            {
                Id = request.PaymentId.Value
            });

            var transRequest = new TransactionRequest
            {
                Amount = amount,
                PaymentMethodNonce = request.PaymentRequest.Nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = gateway.Transaction.Sale(transRequest);
            if(result.IsSuccess())
            {
                payment.ChangePaymentStatus(PaymentStatus.Paid);

                sql = "SELECT [Product].[Name], [ProductOrder].[Quantity], [ProductOrder].[Value], [ProductOrder].[Currency]" +
                          "FROM orders.Products AS [Product], orders.OrderProducts AS [ProductOrder], payments.Payments AS [Payment]" +
                          "WHERE [Payment].[Id] = @Id AND [ProductOrder].[OrderId] = [Payment].[OrderId] AND [Product].[Id] = [ProductOrder].[ProductId]";
                var queryResult = connection.Query<ProductDto>(sql, new { Id = request.PaymentId.Value });

                sql = "SELECT [Customer].[Email] " +
                      "FROM orders.Customers AS [Customer], orders.Orders AS [Order], payments.Payments AS [Payment]" +
                      "WHERE [Payment].[Id] = @Id AND [Payment].[OrderId] = [Order].[Id] AND [Order].[CustomerId] = [Customer].[Id]";
                var customerEmail = await connection.QueryFirstAsync<string>(sql,
                new
                {
                    Id = request.PaymentId.Value
                });

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Payment successfully finished.");
                stringBuilder.AppendLine("Your products:");
                foreach (var product in queryResult)
                {
                    stringBuilder.AppendLine($"{product.Name} - {product.Quantity} - {product.Value} {product.Currency}");
                }

                EmailMessage emailMessage = new EmailMessage(customerEmail, stringBuilder.ToString(), "Payment successfull");
                await _emailSender.SendEmailAsync(emailMessage);
            }
            return Unit.Value;
        }
    }
}
