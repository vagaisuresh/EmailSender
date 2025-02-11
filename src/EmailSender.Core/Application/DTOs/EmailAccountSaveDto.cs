namespace EmailSender.Core.Application.DTOs;

public class EmailAccountSaveDto
{
    public string? Name { get; set; }
    public string? EmailAddress { get; set; }
    public string? OutgoingServer { get; set; }
    public int OutgoingPortNumber { get; set; }
    public bool RequiresAuthentication { get; set; }
    public byte EncryptedConnection { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public bool IsActive { get; set; }
}