using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Application.Interfaces;

public interface IMessageService
{
    Task<IEnumerable<Message>> GetMessagesAsync();
    Task<Message?> GetMessageAsync(int messageId);
}