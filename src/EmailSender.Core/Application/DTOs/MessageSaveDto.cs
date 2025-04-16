using System.ComponentModel.DataAnnotations;

namespace EmailSender.Core.Application.DTOs;

public class MessageSaveDto
{
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }

    [Required]
    [StringLength(100)]
    public string? FromAddress { get; set; }

    [Required]
    [StringLength(100)]
    public string? ReplyToAddress { get; set; }

    [Required]
    [StringLength(100)]
    public string? ToAddress { get; set; }

    [Required]
    [StringLength(100)]
    public string? CCAddress { get; set; }

    [Required]
    [StringLength(100)]
    public string? BCCAddress { get; set; }

    [Required]
    public byte Priority { get; set; }

    [Required]
    [StringLength(250)]
    public string?  Subject { get; set; }

    [Required]
    [StringLength(4000)]
    public string? Content { get; set; }

    [Required]
    public short EmailAccountId { get; set; }

    public ICollection<MessageAttachmentSaveDto>? MessageAttachments { get; set; } = new List<MessageAttachmentSaveDto>();
    public ICollection<MessageRecipientSaveDto>? MessageRecipients { get; set; } = new List<MessageRecipientSaveDto>();
}