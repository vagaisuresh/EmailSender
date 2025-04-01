namespace EmailSender.Core.Application.DTOs;

public class MessageAttachmentDto
{
    public int Id { get; set; }
    public int MessageId { get; set; }
    public string? Attachment { get; set; }
}