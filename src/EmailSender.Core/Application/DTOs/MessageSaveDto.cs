namespace EmailSender.Core.Application.DTOs;

public class MessageSaveDto
{
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
}