using CQRS.Application.Configuration.Commands;
using CQRS.Application.Configuration.Emails;
using CQRS.Domain.Payments;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.Payments.SendEmailAfterPayment
{
    public class SendEmailAfterPaymentCommandHandler : ICommandHandler<SendEmailAfterPaymentCommand, Unit>
    {
        private readonly IEmailSender _emailSender;
        private readonly IPaymentRepository _paymentRepository;

        public SendEmailAfterPaymentCommandHandler(
            IEmailSender emailSender,
            IPaymentRepository paymentRepository)
        {
            _emailSender = emailSender;
            _paymentRepository = paymentRepository;
        }

        public async Task<Unit> Handle(SendEmailAfterPaymentCommand request, CancellationToken cancellationToken)
        {
            // TODO: email
            var emailMessage = new EmailMessage("to@email.com", "content", "Payment");
            await _emailSender.SendEmailAsync(emailMessage);
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);
            payment.MarkEmailNotificationIsSent();
            return Unit.Value;
        }
    }
}