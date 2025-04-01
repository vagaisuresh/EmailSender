namespace EmailSender.Core.Domain.Entities;

public class MessageRecipient
{
    public int Id { get; set;}
    public int MessageId { get; set; }
    public int ContactId { get; set; }
    public bool Status { get; set; }

    public ContactMaster? ContactMasterNavigation { get; set; }
}