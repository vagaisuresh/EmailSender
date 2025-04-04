using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Application.Interfaces;

public interface IMessageAttachmentService
{
    Task<MessageAttachment> GetAttachmentByIdAsync(int attachmentId);
    Task<MessageAttachment> CreateAttachmentAsync(MessageAttachment attachment);
    Task UpdateAttachmentAsync(int id, MessageAttachment attachment);
    Task DeleteAttachmentAsync(int id);
}