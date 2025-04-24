using EmailSender.Core.Application.DTOs;
using EmailSender.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IBulkEmailService _bulkEmailService;

    public EmailController(IBulkEmailService bulkEmailService)
    {
        _bulkEmailService = bulkEmailService;
    }

    [HttpPost("send-bulk")]
    public async Task<IActionResult> SendBulkEmailsAsync([FromBody] BulkEmailRequest bulkEmailRequest)
    {
        try
        {
            await _bulkEmailService.SendBulkEmailsAsync(bulkEmailRequest.Name, 
                bulkEmailRequest.FromAddress, 
                bulkEmailRequest.Recipients, 
                bulkEmailRequest.Subject, 
                bulkEmailRequest.HtmlBody);
                
            return Ok("Bulk email process started.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex}");
        }
    }
}