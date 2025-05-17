using AutoMapper;
using EmailSender.Core.Application.DTOs;
using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageAttachmentsController : ControllerBase
{
    private readonly IMessageAttachmentService _service;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;
    
    public MessageAttachmentsController(IMessageAttachmentService service, IMapper mapper, ILoggerService logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> PostMessageAttachment([FromBody] MessageAttachmentSaveDto messageAttachmentSaveDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid model received.");

        try
        {
            var messageAttachment = _mapper.Map<MessageAttachmentSaveDto, MessageAttachment>(messageAttachmentSaveDto);
            var savedAttachment = await _service.CreateAttachmentAsync(messageAttachment);

            if (savedAttachment == null)
                return NotFound();

            var attachmentDto = _mapper.Map<MessageAttachment, MessageAttachmentDto>(savedAttachment);
            return Ok(attachmentDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while saving the attachment in PostMessageAttachment method: {ex}");
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMessageAttachment([FromRoute] int id)
    {
        if (id == 0)
            return BadRequest("Invalid ID provided.");
        
        try
        {
            await _service.DeleteAttachmentAsync(id);
            return NoContent();
        }
        catch(Exception ex)
        {
            _logger.LogError($"An error occurred while deleting the attachment DeleteMessageAttachment method: {ex}");
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }
}