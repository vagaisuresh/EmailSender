using System.Net;
using System.Net.Mail;
using EmailSender.Core.Application.Common.Models;
using EmailSender.Core.Application.Interfaces;

namespace EmailSender.Core.Infrastructure.Services;

public class SmtpEmailService : IEmailSenderService
{
    public async Task SendEmailAsync(NewSmtpMessage newSmtpMessage, EmailConfiguration emailConfiguration)
    {
        var mailMessage = CreateMessage(newSmtpMessage);
        await SendAsync(mailMessage, emailConfiguration);
    }

    private MailMessage CreateMessage(NewSmtpMessage newSmtpMessage)
    {
        if (string.IsNullOrEmpty(newSmtpMessage.From))
            throw new InvalidOperationException("The 'From' email address is required.");
        
        newSmtpMessage.To  ??= string.Empty;
        newSmtpMessage.CC  ??= string.Empty;
        newSmtpMessage.BCC  ??= string.Empty;

        MailMessage mailMessage= new MailMessage();
        mailMessage.From = new MailAddress(newSmtpMessage.From);

        foreach (var email in newSmtpMessage.To)
        {
            mailMessage.To.Add(email.ToString());
        }

        foreach (var email in newSmtpMessage.CC)
        {
            mailMessage.CC.Add(email.ToString());
        }

        foreach (var email in newSmtpMessage.BCC)
        {
            mailMessage.Bcc.Add(email.ToString());
        }
        
        mailMessage.Subject = newSmtpMessage.Subject;
        mailMessage.Body = string.Format("{0}", newSmtpMessage.Content);
        mailMessage.IsBodyHtml = true;

        return mailMessage;
    }

    private async Task SendAsync(MailMessage mailMessage, EmailConfiguration emailConfiguration)
    {
        if (string.IsNullOrEmpty(emailConfiguration.SmtpServer))
            throw new InvalidOperationException("The SMTP Server is required.");
        
        using (var smtpClient = new SmtpClient())
        {
            smtpClient.Host = emailConfiguration.SmtpServer;
            smtpClient.Port = emailConfiguration.Port;
            smtpClient.EnableSsl = emailConfiguration.EncryptedConnection;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(emailConfiguration.UserName, emailConfiguration.Password);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Failed to send email.", ex);
            }
            finally
            {
                smtpClient.Dispose();
                mailMessage.Dispose();
            }
        }
    }
}