using AutoMapper;
using EmailSender.Core.Application.DTOs;
using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailAccountsController : ControllerBase
{
    private readonly IEmailAccountService _service;
    private readonly IMapper _mapper;

    public EmailAccountsController(IEmailAccountService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
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
            
            var emailAccountDto = _mapper.Map<EmailAccountDto>(emailAccount);

            return Ok(emailAccountDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }
}