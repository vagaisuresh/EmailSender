using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Application.Interfaces;

public interface IMessageRecipientService
{
    Task<MessageRecipient> GetMessageRecipientByIdAsync(int messageId);
    Task<MessageRecipient> CreateMessageRecipientAsync(MessageRecipient messageRecipient);
    Task<MessageRecipient> UpdateMessageRecipientAsync(int id, MessageRecipient messageRecipient);
    Task<MessageRecipient> DeleteMessageRecipientAsync(int id);
}