namespace EmailSender.Core.Application.Interfaces;

public interface IBulkEmailService
{
    Task SendBulkEmailsAsync(int messageId);
}