using CQRS.Application.Configuration.Emails;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(EmailMessage message)
        {
            // Integration with email service.

            return;
        }
    }
}