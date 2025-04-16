using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Domain.Repositories;

public interface IMessageAttachmentRepository
{
    Task<MessageAttachment?> GetAttachmentByIdAsync(int id);
    Task AddAsync(MessageAttachment messageAttachment);
    //Task AddRangeAsync(ICollection<MessageAttachment> messageAttachments);
    void Update(MessageAttachment messageAttachment);
    void Remove(MessageAttachment messageAttachment);
}