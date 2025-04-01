namespace EmailSender.Core.Application.DTOs;

public class MessageRecipientSaveDto
{
    public int MessageId { get; set; }
    public int ContactId { get; set; }
    public bool Status { get; set; }
}