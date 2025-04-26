using EmailSender.Core.Application.DTOs;
using EmailSender.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IBulkEmailService _bulkEmailService;
    private readonly ILoggerService _logger;

    public EmailController(IBulkEmailService bulkEmailService, ILoggerService logger)
    {
        _bulkEmailService = bulkEmailService;
        _logger = logger;
    }

    [HttpPost("send-bulk")]
    public async Task<IActionResult> SendBulkEmailsAsync([FromBody] BulkEmailRequest bulkEmailRequest)
    {
        try
        {
            await _bulkEmailService.SendBulkEmailsAsync(bulkEmailRequest.MessageId);
            return Ok("Bulk email process started.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while sending bulk emails in SendBulkEmailsAsync method. {ex}");
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }
}