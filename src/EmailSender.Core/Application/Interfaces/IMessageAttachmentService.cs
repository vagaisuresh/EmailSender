using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Application.Interfaces;

public interface IMessageAttachmentService
{
    Task<MessageAttachment> CreateAttachmentAsync(MessageAttachment attachment);
    Task DeleteAttachmentAsync(int id);
}