using EmailSender.Core.Domain.Entities;

namespace EmailSender.Core.Application.DTOs;

public class MessageRecipientDto
{
    public int Id { get; set;}
    public int MessageId { get; set; }
    public int ContactId { get; set; }
    public bool Status { get; set; }

    public ContactMaster? ContactMasterNavigation { get; set; }
}