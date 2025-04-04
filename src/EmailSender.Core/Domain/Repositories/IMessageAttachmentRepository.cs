using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Domain.Repositories;

public interface IMessageAttachmentRepository
{
    Task<MessageAttachment?> GetAttachmentByIdAsync(int id);
    Task AddAsync(MessageAttachment messageAttachment);
    void Update(MessageAttachment messageAttachment);
    void Remove(MessageAttachment messageAttachment);
}