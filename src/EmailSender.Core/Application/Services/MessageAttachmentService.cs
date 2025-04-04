using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;

namespace EmailSender.Core.Application.Services;

public class MessageAttachmentService : IMessageAttachmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggerService _logger;

    public MessageAttachmentService(IUnitOfWork unitOfWork, ILoggerService logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public Task<MessageAttachment> GetAttachmentByIdAsync(int attachmentId)
    {
        throw new NotImplementedException();
    }

    public Task<MessageAttachment> CreateAttachmentAsync(MessageAttachment attachment)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAttachmentAsync(int id, MessageAttachment attachment)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAttachmentAsync(int id)
    {
        throw new NotImplementedException();
    }
}