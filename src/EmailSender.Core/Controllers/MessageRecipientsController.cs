using AutoMapper;
using EmailSender.Core.Application.DTOs;
using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageRecipientsController : ControllerBase
{
    private readonly IMessageRecipientService _service;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;

    public MessageRecipientsController(IMessageRecipientService service, IMapper mapper, ILoggerService logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> PostMessageRecipientAsync([FromBody] MessageRecipientSaveDto messageRecipientSaveDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid model received.");

        try
        {
            var recipient = _mapper.Map<MessageRecipientSaveDto, MessageRecipient>(messageRecipientSaveDto);
            var savedRecipient = await _service.CreateMessageRecipientAsync(recipient);

            if (savedRecipient == null)
                return NotFound();

            var recipientDto = _mapper.Map<MessageRecipient, MessageRecipientDto>(savedRecipient);
            return Ok(recipientDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while saving recipient in PostMessageRecipientAsync method: {ex}");
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMessageRecipientAsync(int id)
    {
        if (id == 0)
            return BadRequest("Invalid ID provided.");

        try
        {
            await _service.DeleteMessageRecipientAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while saving recipient in PostMessageRecipientAsync method: {ex}");
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }
}