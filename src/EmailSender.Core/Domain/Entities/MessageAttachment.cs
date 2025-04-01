namespace EmailSender.Core.Domain.Entities;

public class MessageAttachment
{
    public int Id { get; set; }
    public int MessageId { get; set; }
    public string? Attachment { get; set; }
}