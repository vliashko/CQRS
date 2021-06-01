using CQRS.Application.Configuration.Commands;
using CQRS.Application.Configuration.Data;
using CQRS.Application.Configuration.Emails;
using CQRS.Domain.Payments;
using Dapper;
using MediatR;
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
            // TODO: work with email (Payment)

            //var connection = _sqlConnectionFactory.GetOpenConnection();

            //const string sql = "SELECT [Customer].[Email] " +
            //                   "FROM orders.v_Customers AS [Customer] " +
            //                   "WHERE [Customer].[Id] = @Id";

            //var customerEmail = await connection.QueryFirstAsync<string>(sql,
            //    new
            //    {
            //        Id =
            //    });


            //var emailMessage = new EmailMessage("vladimir.lyashko02@gmail.com", "You need to pay for order.", "Payment");
            //await _emailSender.SendEmailAsync(emailMessage);
            //var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);
            //payment.MarkEmailNotificationIsSent();
            return Unit.Value;
        }
    }
}