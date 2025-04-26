using AutoMapper;
using EmailSender.Core.Application.DTOs;
using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _service;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;

    public AccountsController(IAccountService service, IMapper mapper, ILoggerService logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmailAccointsAsync()
    {
        try
        {
            var emailAccounts = await _service.GetEmailAccountsAsync();

            if (emailAccounts == null || !emailAccounts.Any())
                return NoContent();

            var emailAccountDtos = _mapper.Map<IEnumerable<EmailAccount>, IEnumerable<EmailAccountDto>>(emailAccounts);

            return Ok(emailAccountDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting email accounts in GetEmailAccointsAsync method: {ex}");
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }

    [HttpGet("{id}", Name = "GetEmailAccount")]
    public async Task<IActionResult> GetEmailAccountAsync(short id)
    {
        if (id == 0)
            return BadRequest("Invalid ID provided.");

        try
        {
            var emailAccount = await _service.GetEmailAccountByIdAsync(id);

            if (emailAccount == null)
                return NotFound();

            var emailAccountDto = _mapper.Map<EmailAccountDto>(emailAccount);

            return Ok(emailAccountDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting email account in GetEmailAccountAsync method: {ex}");
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostEmailAccountAsync([FromBody] EmailAccountSaveDto emailAccountSaveDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid model received.");

        try
        {
            var emailAccount = _mapper.Map<EmailAccountSaveDto, EmailAccount>(emailAccountSaveDto);
            var savedEmailAccount = await _service.CreateEmailAccountAsync(emailAccount);

            if (savedEmailAccount == null)
                return NotFound();

            var emailAccountDto = _mapper.Map<EmailAccount, EmailAccountDto>(savedEmailAccount);

            return CreatedAtRoute("GetEmailAccount", new { emailAccountDto.Id }, emailAccountDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while saving email account in PostEmailAccountAsync method: {ex}");
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmailAccountAsync([FromRoute] short id, [FromBody] EmailAccountSaveDto emailAccountSaveDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid model received.");

        try
        {
            var emailAccount = _mapper.Map<EmailAccount>(emailAccountSaveDto);
            await _service.UpdateEmailAccountAsync(id, emailAccount);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while updating email account in PutEmailAccountAsync method: {ex}");
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmailAccountAsync([FromRoute] short id)
    {
        if (id == 0)
            return BadRequest("Invalid id received.");

        try
        {
            await _service.DeleteEmailAccountAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while deleting email account in DeleteEmailAccountAsync method: {ex}");
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }
}