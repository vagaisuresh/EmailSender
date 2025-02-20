using System.ComponentModel.DataAnnotations;

namespace EmailSender.Core.Application.DTOs;

public class ContactMasterSaveDto
{
    [Required]
    [Range(1, short.MaxValue)]
    public short GroupId { get; set; }

    [Required]
    [StringLength(5)]
    public string? Salutation { get; set; }

    [Required]
    [StringLength(50)]
    public string? FullName { get; set; }

    [StringLength(50)]
    public string? JobTitle { get; set; }

    [StringLength(200)]
    public string? CompanyName { get; set; }

    [StringLength(250)]
    public string? Address { get; set; }

    [Required]
    [StringLength(5)]
    public string? MobileIsd { get; set; }

    [Required]
    [StringLength(15)]
    public string? MobileNumber { get; set; }

    [Required]
    [StringLength(100)]
    public string? EmailAddress { get; set; }
    
    public bool IsActive { get; set; }
}