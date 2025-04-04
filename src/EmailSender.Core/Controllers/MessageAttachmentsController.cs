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

    [HttpGet("{id}", Name = "PostMessageAttachment")]
    public async Task<IActionResult> GetMessageAttachmentByIdAsync(int id)
    {
        try
        {
            var attachment = await _service.GetAttachmentByIdAsync(id);

            if (attachment == null)
                return NoContent();

            var attachmentDto = _mapper.Map<MessageAttachment, MessageAttachmentDto>(attachment);

            return Ok(attachmentDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting the attachment in GetMessageAttachmentByIdAsync method: {ex}");
            return StatusCode(500, "Internal server error. Please try again later.");
        }
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
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMessageAttachment([FromRoute] int id, [FromBody] MessageAttachmentSaveDto messageAttachmentSaveDto)
    {
        if (id == 0)
            return BadRequest("Invalid ID received.");
        
        if (!ModelState.IsValid)
            return BadRequest("Invalid model received.");
        
        try
        {
            var messageAttachment = _mapper.Map<MessageAttachmentSaveDto, MessageAttachment>(messageAttachmentSaveDto);
            await _service.UpdateAttachmentAsync(id, messageAttachment);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while updating the attachment in PutMessageAttachment method: {ex}");
            return StatusCode(500, "Internal server error. Please try again later.");
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
            return StatusCode(500, "Internal server error. Please try again later.");
        }
    }
}