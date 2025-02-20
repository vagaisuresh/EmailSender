using System.ComponentModel.DataAnnotations;

namespace EmailSender.Core.Application.DTOs;

public class ContactGroupSaveDto
{
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string? GroupName { get; set; }

    [Required]
    [StringLength(200)]
    public string? Description { get; set; }

    public bool IsActive { get; set; }
}