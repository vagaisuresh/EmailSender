namespace EmailSender.Core.Domain.Entities;

public class Message
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? FromAddress { get; set; }
    public string? ReplyToAddress { get; set; }
    public string? ToAddress { get; set; }
    public string? CCAddress { get; set; }
    public string? BCCAddress { get; set; }
    public byte Priority { get; set; }
    public string?  Subject { get; set; }
    public string? Content { get; set; }
    public short EmailAccountId { get; set; }
    public bool Prepared { get; set; }
    public short PreparedBy { get; set; }
    public DateTime PreparedDateTime { get; set; }
    public bool Emailed { get; set; }
    public DateTime EmailedDateTime { get; set; }

    public EmailAccount? EmailAccountNavigation { get; set; }

    public IEnumerable<MessageAttachment> MessageAttachments { get; set; } = null!;
    public IEnumerable<MessageRecipient> MessageRecipients  { get; set; } = null!;
}