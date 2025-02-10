using EmailSender.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailAccountsController : ControllerBase
{
    private readonly IEmailAccountService _service;
    public EmailAccountsController(IEmailAccountService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmailAccointsAsync()
    {
        try
        {
            var emailAccounts = await _service.GetEmailAccountsAsync();

            if (emailAccounts == null || !emailAccounts.Any())
                return NoContent();
            
            return Ok(emailAccounts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmailAccountAsync(short id)
    {
        if (id == 0)
            return BadRequest("Invalid ID provided.");

        try
        {
            var emailAccount = await _service.GetEmailAccountByIdAsync(id);

            if (emailAccount == null)
                return NotFound();
            
            return Ok(emailAccount);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }
}