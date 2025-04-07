using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;

namespace EmailSender.Core.Application.Services;

public class MessageRecipientService : IMessageRecipientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggerService _logger;

    public MessageRecipientService(IUnitOfWork unitOfWork, ILoggerService logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public Task<MessageRecipient?> GetMessageRecipientByIdAsync(int id)
    {
        try
        {
            return _unitOfWork.MessageRecipientRepository.GetMessageRecipientByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting recipient in GetMessageRecipientByIdAsync service method: {ex}");
            throw new Exception("An error occurred while getting the recipient.");
        }
    }

    public async Task<MessageRecipient> CreateMessageRecipientAsync(MessageRecipient messageRecipient)
    {
        try
        {
            await _unitOfWork.MessageRecipientRepository.AddAsync(messageRecipient);
            await _unitOfWork.SaveAsync();

            return messageRecipient;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while saving recipient in CreateMessageRecipientAsync service method: {ex}");
            throw new Exception("An error occurred while saving the recipient.");
        }
    }

    public async Task UpdateMessageRecipientAsync(int id, MessageRecipient messageRecipient)
    {
        var existingRecipient = await _unitOfWork.MessageRecipientRepository.GetMessageRecipientByIdAsync(id);

        if (existingRecipient == null)
            throw new InvalidOperationException("Message recipient not found.");
        
        existingRecipient.MessageId = messageRecipient.MessageId;
        existingRecipient.ContactId = messageRecipient.ContactId;
        existingRecipient.Status = messageRecipient.Status;

        try
        {
            _unitOfWork.MessageRecipientRepository.Update(existingRecipient);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while updating recipient in UpdateMessageRecipientAsync service method: {ex}");
            throw new Exception("An error occurred while updating the recipient.");
        }
    }

    public async Task DeleteMessageRecipientAsync(int id)
    {
        var existingRecipient = await _unitOfWork.MessageRecipientRepository.GetMessageRecipientByIdAsync(id);

        if (existingRecipient == null)
            throw new InvalidOperationException("Message recipient not found.");
        
        try
        {
            _unitOfWork.MessageRecipientRepository.Remove(existingRecipient);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while deleting recipient in DeleteMessageRecipientAsync service method: {ex}");
            throw new Exception("An error occurred while deleting the recipient.");
        }
    }
}