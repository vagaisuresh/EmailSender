using AutoMapper;
using EmailSender.Core.Application.DTOs;
using EmailSender.Core.Application.Interfaces;
using EmailSender.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactGroupsController : ControllerBase
{
    private readonly IContactGroupService _service;
    private readonly IMapper _mapper;

    public ContactGroupsController(IContactGroupService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContactGroups()
    {
        try
        {
            var contactGroups = await _service.GetContactGroupsAsync();

            if (contactGroups == null || !contactGroups.Any())
                return NoContent();

            var contactGroupDtos = _mapper.Map<IEnumerable<ContactGroupMaster>, IEnumerable<ContactGroupDto>>(contactGroups);
            return Ok(contactGroupDtos);
        }
        catch (Exception ex)
        {
            // Log here
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }

    [HttpGet("{id}", Name = "GetContactGroup")]
    public async Task<IActionResult> GetContactGroupById(int id)
    {
        if (id == 0)
            return BadRequest("Invalid ID provided.");

        try
        {
            var contactGroup = await _service.GetContactGroupsByIdAsync(id);

            if (contactGroup == null)
                return NotFound();

            var contactGroupDto = _mapper.Map<ContactGroupMaster, ContactGroupDto>(contactGroup);
            return Ok(contactGroupDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error. Please try again. {ex.Message}");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PostContactGroupAsync([FromBody] ContactGroupSaveDto contactGroupSaveDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data received.");

        try
        {
            var contactGroup = _mapper.Map<ContactGroupSaveDto, ContactGroupMaster>(contactGroupSaveDto);
            var savedGroup = await _service.CreateContactGroupAsync(contactGroup);

            if (savedGroup == null)
                return NotFound();

            var contactGroupDto = _mapper.Map<ContactGroupDto>(savedGroup);
            return CreatedAtRoute("GetContactGroup", new { contactGroupDto.Id, contactGroupDto });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PutContactGroupAsync([FromRoute] int id, [FromBody] ContactGroupSaveDto contactGroupSaveDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data received.");

        try
        {
            var contactGroup = _mapper.Map<ContactGroupMaster>(contactGroupSaveDto);
            await _service.UpdateContactGroupAsync(id, contactGroup);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContactGroupAsync([FromRoute] int id)
    {
        if (id == 0)
            return BadRequest("Invalid id received.");

        try
        {
            await _service.DeleteContactGroupAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error. Please try again later. {ex.Message}");
        }
    }
}