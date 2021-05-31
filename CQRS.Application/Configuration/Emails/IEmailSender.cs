﻿using System.Threading.Tasks;

namespace CQRS.Application.Configuration.Emails
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message);
    }
}