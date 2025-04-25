using EmailSender.Core.Application.Common.Models;
using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Repositories;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailSender.Core.Infrastructure.Services;

public class MailKitEmailSender : IEmailSenderMailKitService
{
    private readonly IUnitOfWork _unitOfWork;

    public MailKitEmailSender(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SendEmailAsync(NewMessage newMessage, EmailConfiguration emailConfiguration)
    {
        var emailMessage = CreateMessage(newMessage, emailConfiguration);
        await SendAsync(emailMessage, emailConfiguration);
    }

    private MimeMessage CreateMessage(NewMessage newMessage, EmailConfiguration emailConfiguration)
    {
        MimeMessage mimeMessage = new MimeMessage();

        mimeMessage.From.Add(new MailboxAddress(emailConfiguration.Name, emailConfiguration.From));
        mimeMessage.To.AddRange(newMessage.To.Select(x => new MailboxAddress("", x)));
        mimeMessage.Cc.AddRange(newMessage.CC.Select(x => new MailboxAddress("", x)));
        mimeMessage.Bcc.AddRange(newMessage.BCC.Select(x => new MailboxAddress("", x)));

        mimeMessage.Subject = newMessage.Subject;
        mimeMessage.Body = new BodyBuilder { HtmlBody = newMessage.HtmlBody }.ToMessageBody();
        
        return mimeMessage;
    }

    private async Task SendAsync(MimeMessage mimeMessage, EmailConfiguration emailConfiguration)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(emailConfiguration.SmtpServer, emailConfiguration.Port, 
                    emailConfiguration.EncryptedConnection ? MailKit.Security.SecureSocketOptions.StartTls : MailKit.Security.SecureSocketOptions.Auto);
                
                await client.AuthenticateAsync(emailConfiguration.UserName, emailConfiguration.Password);
                await client.SendAsync(mimeMessage);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while sending email: {ex}");
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}