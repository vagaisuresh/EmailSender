namespace EmailSender.Core.Application.Interfaces;

public interface IBulkEmailService
{
    //Task SendBulkEmailsAsync(string name, string from, List<string> recipients, string subject, string htmlBody);
    Task SendBulkEmailsAsync(int messageId);
}