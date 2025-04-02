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

    [HttpGet("{id}", Name = "GetMessage")]
    public async Task<IActionResult> GetMessageByIdAsync(int id)
    {
        if (id == 0)
            return BadRequest("Invalid ID provided.");
        
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

    [HttpPost]
    public async Task<IActionResult> PostMessageAsync([FromBody] MessageSaveDto messageSaveDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid model data received.");
        
        try
        {
            var message = _mapper.Map<MessageSaveDto, Message>(messageSaveDto);
            var savedMessage = await _service.CreateMessageAsync(message);

            if (savedMessage == null)
                return NotFound();
            
            var messageDto = _mapper.Map<Message, MessageDto>(savedMessage);
            return CreatedAtRoute("GetMessage", new { id = savedMessage.Id }, messageDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while saving the message in PostMessageAsync method: {ex}");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMessageAsync(int id, [FromBody] MessageSaveDto messageSaveDto)
    {
        if (id == 0)
            return BadRequest("Invalid ID provided.");

        if (!ModelState.IsValid)
            return BadRequest("Invalid model data received.");
        
        try
        {
            var message = _mapper.Map<MessageSaveDto, Message>(messageSaveDto);
            await _service.UpdateMessageAsync(id, message);

            return NoContent();
        }
        catch (Exception ex)
        {
             _logger.LogError($"An error occurred while updating the message in PutMessageAsync method: {ex}");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMessageAsync(int id)
    {
        if (id == 0)
            return BadRequest("Invalid ID provided.");
        
        try
        {
            await _service.DeleteMessageAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
             _logger.LogError($"An error occurred while deleting the message in DeleteMessageAsync method: {ex}");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }
}