namespace EmailSender.Core.Application.DTOs;

public class BulkEmailRequest
{
    public string Name { get; set; } = string.Empty;
    public string FromAddress { get; set; } = string.Empty;

    public List<string> Recipients { get; set; } = new();

    public string Subject { get; set; } = string.Empty;
    public string HtmlBody { get; set; } = string.Empty;
}