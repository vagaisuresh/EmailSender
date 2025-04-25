using EmailSender.Core.Application.Common.Models;

namespace EmailSender.Core.Application.Interfaces;

public interface IEmailSenderMailKitService
{
    /// <summary>
    /// Send an email using the MailKit package.
    /// </summary>
    /// <param name="newMessage"></param>
    /// <param name="emailConfiguration"></param>
    /// <returns></returns>
    Task SendEmailAsync(NewMessage newMessage, EmailConfiguration emailConfiguration);
}