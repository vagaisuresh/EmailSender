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
}