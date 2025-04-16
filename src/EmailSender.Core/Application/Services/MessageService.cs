using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using EmailSender.Core.Domain.Repositories;

namespace EmailSender.Core.Application.Services;

public class MessageService : IMessageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggerService _logger;

    public MessageService(IUnitOfWork unitOfWork, ILoggerService logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync()
    {
        try
        {
            return await _unitOfWork.MessageRepository.GetAllMessagesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting messages in GetMessagesAsync service method: {ex}");
            throw new Exception("An error occurred while getting the messages.");
        }
    }

    public async Task<Message?> GetMessageAsync(int messageId)
    {
        try
        {
            return await _unitOfWork.MessageRepository.GetMessageByIdWithDetailsAsync(messageId);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting message in GetMessageAsync service method: {ex}");
            throw new Exception("An error occurred while getting the message.");
        }
    }

    public async Task<Message> CreateMessageAsync(Message message)
    {
        try
        {
            await _unitOfWork.MessageRepository.AddAsync(message);
            await _unitOfWork.SaveAsync();                          // EF will save message + attachments + recipients
        
            return message;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while saving message in CreateMessageAsync service method: {ex}");
            throw new Exception("An error occurred when saving message.", ex);
        }
    }

    public async Task UpdateMessageAsync(int id, Message message)
    {
        var existingMessage = await _unitOfWork.MessageRepository.GetMessageByIdAsync(id);

        if (existingMessage == null)
            throw new InvalidOperationException("Message not found.");
        
        existingMessage.Name = message.Name;
        existingMessage.FromAddress = message.FromAddress;
        existingMessage.ReplyToAddress = message.ReplyToAddress;
        existingMessage.ToAddress = message.ToAddress;
        existingMessage.CCAddress = message.CCAddress;
        existingMessage.BCCAddress = message.BCCAddress;
        existingMessage.Priority = message.Priority;
        existingMessage.Subject = message.Subject;
        existingMessage.Content = message.Content;
        existingMessage.EmailAccountId = message.EmailAccountId;

        try
        {
            _unitOfWork.MessageRepository.Update(existingMessage);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while updating message in UpdateMessageAsync service method: {ex}");
            throw new Exception("An error occurred when updating message.");
        }
    }

    public async Task DeleteMessageAsync(int id)
    {
        var existingMessage = await _unitOfWork.MessageRepository.GetMessageByIdAsync(id);

        if (existingMessage == null)
            throw new InvalidOperationException("Message not found.");

        try
        {
            _unitOfWork.MessageRepository.Remove(existingMessage);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while deleting message in DeleteMessageAsync service method: {ex}");
            throw new Exception("An error occurred when deleting message.");
        }
    }
}