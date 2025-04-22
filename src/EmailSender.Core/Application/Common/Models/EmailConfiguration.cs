namespace EmailSender.Core.Application.Common.Models;

public class EmailConfiguration
{
    public string? Name { get; set; }
    public string? From { get; set; }
    public string SmtpServer { get; set; } = string.Empty;
    public int Port { get; set; }
    public bool EncryptedConnection { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}