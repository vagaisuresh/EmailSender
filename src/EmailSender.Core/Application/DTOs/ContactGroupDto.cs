namespace EmailSender.Core.Application.DTOs;

public class ContactGroupDto
{
    public short Id { get; set; }
    public string? GroupName { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}