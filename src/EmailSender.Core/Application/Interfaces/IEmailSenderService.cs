using EmailSender.Core.Application.Common.Models;

namespace EmailSender.Core.Application.Interfaces;

public interface IEmailSenderService
{
    Task SendEmailAsync(NewSmtpMessage newSmtpMessage, EmailConfiguration emailConfiguration);
}