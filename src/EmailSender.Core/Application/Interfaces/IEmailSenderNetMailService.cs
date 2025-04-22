using EmailSender.Core.Application.Common.Models;

namespace EmailSender.Core.Application.Interfaces;

public interface IEmailSenderNetMailService
{
    /// <summary>
    /// Send an email using the NET.Mail package.
    /// </summary>
    /// <param name="newMessage"></param>
    /// <param name="emailConfiguration"></param>
    /// <returns></returns>
    Task SendEmailAsync(NewMessage newMessage, EmailConfiguration emailConfiguration);
}