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
    
    public async Task<MessageAttachment?> GetAttachmentByIdAsync(int attachmentId)
    {
        try
        {
            return await _unitOfWork.MessageAttachmentRepository.GetAttachmentByIdAsync(attachmentId);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting the attachment in GetAttachmentByIdAsync service method: {ex}");
            throw new Exception("An error occurred while getting attachment.");
        }
    }

    public async Task<MessageAttachment> CreateAttachmentAsync(MessageAttachment attachment)
    {
        try
        {
            await _unitOfWork.MessageAttachmentRepository.AddAsync(attachment);
            await _unitOfWork.SaveAsync();

            return attachment;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while saving the attachment in CreateAttachmentAsync service method: {ex}");
            throw new Exception("An error occurred while saving attachment.");
        }
    }

    public async Task UpdateAttachmentAsync(int id, MessageAttachment attachment)
    {
        var existingAttachment = await _unitOfWork.MessageAttachmentRepository.GetAttachmentByIdAsync(id);

        if (existingAttachment == null)
            throw new InvalidOperationException("Attachment not found.");

        existingAttachment.MessageId = attachment.MessageId;
        existingAttachment.Attachment = attachment.Attachment;

        try
        {
            _unitOfWork.MessageAttachmentRepository.Update(existingAttachment);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while updating the attachment in UpdateAttachmentAsync service method: {ex}");
            throw new Exception("An error occurred while updating attachment.");
        }
    }

    public async Task DeleteAttachmentAsync(int id)
    {
        var existingAttachment = await _unitOfWork.MessageAttachmentRepository.GetAttachmentByIdAsync(id);

        if (existingAttachment == null)
            throw new InvalidOperationException("Attachment not found.");

        try
        {
            _unitOfWork.MessageAttachmentRepository.Remove(existingAttachment);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while deleting the attachment in DeleteAttachmentAsync service method: {ex}");
            throw new Exception("An error occurred while deleting attachment.");
        }
    }
}