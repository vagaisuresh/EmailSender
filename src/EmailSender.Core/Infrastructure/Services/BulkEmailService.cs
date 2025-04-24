using EmailSender.Core.Application.Common.Models;
using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Repositories;

namespace EmailSender.Core.Infrastructure.Services;

public class BulkEmailService : IBulkEmailService
{
    private readonly IEmailSenderMailKitService _emailSenderService;
    private readonly IUnitOfWork _unitOfWork;

    public BulkEmailService(IEmailSenderMailKitService emailSenderService, IUnitOfWork unitOfWork)
    {
        _emailSenderService = emailSenderService;
        _unitOfWork = unitOfWork;
    }

    public async Task SendBulkEmailsAsync(string name, string from, List<string> recipients, string subject, string htmlBody)
    {
        foreach (var recipient in recipients)
        {
            try
            {
                var newMessage = new NewMessage(name, from, 
                    new string[] { recipient }, 
                    new string[] {}, 
                    new string[] {}, subject, htmlBody);

                var emailConfig = new EmailConfiguration();

                await _emailSenderService.SendEmailAsync(newMessage, emailConfig);
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }
    }
}