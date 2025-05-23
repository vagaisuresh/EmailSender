using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Domain.Repositories;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetMessagesAsync();
    Task<Message?> GetMessageAsync(int id);
    Task<Message?> GetMessageByIdAsync(int id);

    Task AddAsync(Message message);
    void Update(Message message);
    void Remove(Message message);
}