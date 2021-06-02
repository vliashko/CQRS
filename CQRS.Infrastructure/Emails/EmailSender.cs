using CQRS.Application.Configuration.Emails;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        private const string emailLogin = "caloriestracker@mail.ru";
        private const string password = "IteEviSUy43)";

        public async Task SendEmailAsync(EmailMessage message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Administration of Clothes Store", emailLogin));
            emailMessage.To.Add(new MailboxAddress("", message.To));
            emailMessage.Subject = message.Subject;

            BodyBuilder bb = new BodyBuilder
            {
                TextBody = message.Content
            };
            emailMessage.Body = bb.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.mail.ru", 465, true);
            await client.AuthenticateAsync(emailLogin, password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);

            return;
        }
    }
}