using AutoMapper;
using EmailSender.Core.Application.DTOs;
using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactService _service;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;

    public ContactsController(IContactService service, IMapper mapper, ILoggerService logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var contacts = await _service.GetContactsAsync();

            if (contacts == null || !contacts.Any())
                return NoContent();

            var contactsDto = _mapper.Map<IEnumerable<ContactMasterDto>>(contacts);
            return Ok(contactsDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting contacts in GetAllAsync method: {ex}");
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }

    [HttpGet("{id}", Name = "GetContact")]
    public async Task<IActionResult> GetContactAsync(int id)
    {
        try
        {
            var contact = await _service.GetContactAsync(id);

            if (contact == null)
                return NotFound();
            
            var contactDto = _mapper.Map<ContactMaster, ContactMasterDto>(contact);
            return Ok(contactDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while getting contact in GetByIdAsync method: {ex}");
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PostAsync([FromBody] ContactMasterSaveDto contactMasterSaveDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data received.");
        
        try
        {
            var contact = _mapper.Map<ContactMasterSaveDto, ContactMaster>(contactMasterSaveDto);
            var contactCreated = await _service.SaveContactAsync(contact);

            if (contactCreated == null)
                return NotFound();
            
            var contactDto = _mapper.Map<ContactMasterDto>(contactCreated);
            return CreatedAtRoute("GetContact", new { contactDto.Id , contactDto });
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while saving contact in PostAsync method: {ex}");
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PutAsync(int id, [FromBody] ContactMasterSaveDto contactMasterSaveDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data received.");
        
        try
        {
            var contact = _mapper.Map<ContactMasterSaveDto, ContactMaster>(contactMasterSaveDto);
            await _service.UpdateContactAsync(id, contact);
            
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while updating contact in PutAsync method: {ex}");
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        if (id == 0)
            return BadRequest("Invalid id received.");
        
        try
        {
            await _service.DeleteContactAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while deleting contact in DeleteAsync method: {ex}");
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }
}