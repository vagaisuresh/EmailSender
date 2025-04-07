using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Domain.Repositories;

public interface IMessageRecipientRepository
{
    Task<MessageRecipient?> GetMessageRecipientByIdAsync(int id);
    Task AddAsync(MessageRecipient messageRecipient);
    void Update(MessageRecipient messageRecipient);
    void Remove(MessageRecipient messageRecipient);
}