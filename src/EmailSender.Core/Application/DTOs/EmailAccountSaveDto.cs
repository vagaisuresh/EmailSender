using System.ComponentModel.DataAnnotations;

namespace EmailSender.Core.Application.DTOs;

public class EmailAccountSaveDto
{
    [Required]
    [StringLength(50)]
    public string? Name { get; set; }

    [Required]
    [StringLength(100)]
    public string? EmailAddress { get; set; }

    [Required]
    [StringLength(100)]
    public string? SmtpServer { get; set; }

    [Required]
    public int Port { get; set; }

    [Required]
    public bool RequiresAuthentication { get; set; }

    [Required]
    [Range(0, 5)]
    public byte EncryptedConnection { get; set; }

    [Required]
    [StringLength(100)]
    public string? UserName { get; set; }

    [Required]
    [StringLength(20)]
    public string? Password { get; set; }

    public bool IsActive { get; set; }
}