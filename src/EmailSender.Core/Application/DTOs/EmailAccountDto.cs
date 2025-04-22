namespace EmailSender.Core.Application.DTOs;

public class EmailAccountDto
{
    public short Id { get; set; }
    public string? Name { get; set; }
    public string? EmailAddress { get; set; }
    public string? SmtpServer { get; set; }
    public int Port { get; set; }
    public bool RequiresAuthentication { get; set; }
    public byte EncryptedConnection { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public bool IsActive { get; set; }
}