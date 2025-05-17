using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Application.Interfaces;

public interface IMessageRecipientService
{
    Task<MessageRecipient> CreateMessageRecipientAsync(MessageRecipient messageRecipient);
    Task DeleteMessageRecipientAsync(int id);
}