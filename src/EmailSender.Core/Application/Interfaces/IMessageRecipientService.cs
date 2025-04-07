using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Application.Interfaces;

public interface IMessageRecipientService
{
    Task<MessageRecipient?> GetMessageRecipientByIdAsync(int id);
    Task<MessageRecipient> CreateMessageRecipientAsync(MessageRecipient messageRecipient);
    Task UpdateMessageRecipientAsync(int id, MessageRecipient messageRecipient);
    Task DeleteMessageRecipientAsync(int id);
}