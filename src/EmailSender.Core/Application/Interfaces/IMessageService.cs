using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Application.Interfaces;

public interface IMessageService
{
    Task<IEnumerable<Message>> GetMessagesAsync();
    Task<Message?> GetMessageAsync(int id);
    Task<Message> CreateMessageAsync(Message message);
    Task UpdateMessageAsync(int id, Message message);
    Task DeleteMessageAsync(int id);
}