namespace EmailSender.Core.Application.DTOs;

public class MessageAttachmentSaveDto
{
    public int MessageId { get; set; }
    public string? Attachment { get; set; }
}