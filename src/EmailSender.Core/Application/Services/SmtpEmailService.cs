using System.Net;
using System.Net.Mail;
using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Application.Services;

public class SmtpEmailService : IEmailService
{
    private readonly ILoggerService _logger;

    public SmtpEmailService(ILoggerService logger)
    {
        _logger = logger;
    }

    public async Task SendEmailAsync(SmtpMessage smtpMessage, EmailConfiguration config)
    {
        var mailMessage = await ComposeMessageAsync(smtpMessage);
        await SendAsync(mailMessage, config);
    }

    private async Task<MailMessage> ComposeMessageAsync(SmtpMessage smtpMessage)
    {
        var mailMessage = new MailMessage();

        mailMessage.From = new MailAddress(smtpMessage.From ?? string.Empty, smtpMessage.Name);

        if (!string.IsNullOrWhiteSpace(smtpMessage.To))
        {
            string[] tos = smtpMessage.To.Split(',', ';');
            foreach (string address in tos)
            {
                mailMessage.To.Add(address);
            }
        }

        if (!string.IsNullOrWhiteSpace(smtpMessage.CC))
        {
            string[] ccs = smtpMessage.CC.Split(',', ';');
            foreach (string address in ccs)
            {
                mailMessage.CC.Add(address);
            }
        }

        if (!string.IsNullOrWhiteSpace(smtpMessage.BCC))
        {
            string[] bccs = smtpMessage.BCC.Split(',', ';');
            foreach (string address in bccs)
            {
                mailMessage.Bcc.Add(address);
            }
        }

        mailMessage.Subject = smtpMessage.Subject;

        if (smtpMessage.Attachments != null && smtpMessage.Attachments.Any())
        {
            foreach (var attachment in smtpMessage.Attachments)
            {
                try
                {
                    using (var ms = new MemoryStream())
                    {
                        await attachment.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        var mailAttachment = new Attachment(new MemoryStream(fileBytes), attachment.FileName, attachment.ContentType);
                        mailMessage.Attachments.Add(mailAttachment);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occurred while processing the attachment in ComposeMessageAsync service method: {ex}");
                    throw new InvalidOperationException("An error occurred while processing attachment.");
                }
            }
        }

        mailMessage.Body = smtpMessage.Content;
        return mailMessage;
    }

    private async Task SendAsync(MailMessage mailMessage, EmailConfiguration emailConfiguration)
    {
        using (var smtpClient = new SmtpClient())
        {
            smtpClient.Host = emailConfiguration.SmtpServer ?? string.Empty;
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
                _logger.LogError($"An error occurred while sending email in SendAsync service method: {ex}");
                throw new InvalidOperationException("Failed to send email.");
            }
            finally
            {
                smtpClient.Dispose();
                mailMessage.Dispose();
            }
        }
    }
}