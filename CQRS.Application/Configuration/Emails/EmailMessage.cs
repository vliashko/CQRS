namespace CQRS.Application.Configuration.Emails
{
    public struct EmailMessage
    {
        public string To { get; }

        public string Content { get; }

        public string Subject { get; }

        public EmailMessage(
            string to,
            string content,
            string subject)
        {
            To = to;
            Content = content;
            Subject = subject;
        }
    }
}