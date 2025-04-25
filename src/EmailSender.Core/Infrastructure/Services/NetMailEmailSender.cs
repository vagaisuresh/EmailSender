using System.Net;
using System.Net.Mail;
using EmailSender.Core.Application.Common.Models;
using EmailSender.Core.Application.Interfaces;

namespace EmailSender.Core.Infrastructure.Services;

public class NetMailEmailSender : IEmailSenderNetMailService
{
    public async Task SendEmailAsync(NewMessage newMessage, EmailConfiguration emailConfiguration)
    {
        var mailMessage = CreateMessage(newMessage);
        await SendAsync(mailMessage, emailConfiguration);
    }

    private MailMessage CreateMessage(NewMessage newMessage)
    {
        if (string.IsNullOrEmpty(newMessage.From))
            throw new InvalidOperationException("The 'From' email address is required.");
        
        MailMessage mailMessage= new MailMessage();
        mailMessage.From = new MailAddress(newMessage.From);

        foreach (var email in newMessage.To)
        {
            mailMessage.To.Add(email.ToString());
        }

        foreach (var email in newMessage.CC)
        {
            mailMessage.CC.Add(email.ToString());
        }

        foreach (var email in newMessage.BCC)
        {
            mailMessage.Bcc.Add(email.ToString());
        }
        
        mailMessage.Subject = newMessage.Subject;
        mailMessage.Body = string.Format("{0}", newMessage.HtmlBody);
        mailMessage.IsBodyHtml = true;

        return mailMessage;
    }

    private async Task SendAsync(MailMessage mailMessage, EmailConfiguration emailConfiguration)
    {
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