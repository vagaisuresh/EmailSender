using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(SmtpMessage smtpMessage, EmailConfiguration emailConfiguration);
}