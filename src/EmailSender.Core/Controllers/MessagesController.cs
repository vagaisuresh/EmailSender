using AutoMapper;
using EmailSender.Core.Application.DTOs;
using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _service;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;

    public MessagesController(IMessageService service, IMapper mapper, ILoggerService logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetMessagesAsync()
    {
        try
        {
            var messages = await _service.GetMessagesAsync();

            if (messages == null)
                return NoContent();

            var messageDtos = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(messages);
            return Ok(messageDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting messages in GetMessagesAsync method: {ex}");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMessageByIdAsync(int id)
    {
        try
        {
            var message = await _service.GetMessageAsync(id);

            if (message == null)
                return NotFound("Message not found.");
            
            var messageDto = _mapper.Map<Message, MessageDto>(message);
            return Ok(messageDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting message in GetMessageByIdAsync method: {ex}");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }
}