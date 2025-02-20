namespace EmailSender.Core.Domain.Entities;

public class ContactMaster
{
    public int Id { get; set;}
    public short GroupId { get; set; }
    public string? Salutation { get; set; }
    public string? FullName { get; set; }
    public string? JobTitle { get; set; }
    public string? CompanyName { get; set; }
    public string? Address { get; set; }
    public string? MobileIsd { get; set; }
    public string? MobileNumber { get; set; }
    public string? EmailAddress { get; set; }
    public bool IsActive { get; set; }
    public bool IsRemoved { get; set; }

    public ContactGroupMaster? ContactGroupMaster { get; set; }
}