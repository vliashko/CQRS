using CQRS.Application.Configuration.Commands;
using CQRS.Application.Configuration.Data;
using CQRS.Application.Configuration.Emails;
using CQRS.Domain.Customers.Orders;
using CQRS.Domain.Payments;
using Dapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.Payments.SendEmailAfterPayment
{
    public class SendEmailAfterPaymentCommandHandler : ICommandHandler<SendEmailAfterPaymentCommand, Unit>
    {
        private readonly IEmailSender _emailSender;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public SendEmailAfterPaymentCommandHandler(
            IEmailSender emailSender,
            IPaymentRepository paymentRepository,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _emailSender = emailSender;
            _paymentRepository = paymentRepository;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Unit> Handle(SendEmailAfterPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);

            var connection = _sqlConnectionFactory.GetOpenConnection();

            string sql = "SELECT [Customer].[Email] " +
                               "FROM orders.Customers AS [Customer], orders.Orders AS [Order], payments.Payments AS [Payment]" +
                               "WHERE [Payment].[Id] = @Id AND [Payment].[OrderId] = [Order].[Id] AND [Order].[CustomerId] = [Customer].[Id]";
            var customerEmail = await connection.QueryFirstAsync<string>(sql,
            new
            {
                Id = request.PaymentId.Value
            });

            sql = "SELECT [Payment].[OrderId] " +
                   "FROM payments.Payments AS [Payment]" +
                   "WHERE [Payment].[Id] = @Id";
            var orderId = await connection.QueryFirstAsync<Guid>(sql,
            new
            {
                Id = request.PaymentId.Value
            });

            var emailMessage = new EmailMessage(customerEmail, 
                PaymentNotificationService.GetPaymentEmailConfirmationDescription(payment.Id, new OrderId(orderId)), 
                "Payment");
            await _emailSender.SendEmailAsync(emailMessage);
            payment.MarkEmailNotificationIsSent();

            return Unit.Value;
        }
    }
}