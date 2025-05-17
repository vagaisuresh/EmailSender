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

    public async Task SendBulkEmailsAsync(int messageId) //string name, string from, List<string> recipients, string subject, string htmlBody)
    {
        var message = await _unitOfWork.MessageRepository.GetMessageAsync(messageId);

        if (message == null)
            throw new InvalidOperationException("Message not found.");
        
        foreach (var recipient in message.MessageRecipients)
        {
            if (recipient.ContactMasterNavigation == null || recipient.ContactMasterNavigation.EmailAddress == null)
                continue;

            try
            {
                var newMessage = new NewMessage(message.FromAddress, message.Name, 
                    new string[] { recipient.ContactMasterNavigation.EmailAddress }, 
                    new string[] {}, 
                    new string[] {}, 
                    message.Subject, message.Content);

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