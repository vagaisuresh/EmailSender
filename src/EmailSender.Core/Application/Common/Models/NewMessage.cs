namespace EmailSender.Core.Application.Common.Models;

public class NewMessage
{
    public string? From { get; set; } = string.Empty;
    public string? Name { get; set; }

    public IEnumerable<string> To { get; set; }
    public IEnumerable<string> CC { get; set; }
    public IEnumerable<string> BCC { get; set; }

    public string? Subject { get; set; }
    public string? HtmlBody { get; set; }

    public NewMessage(string? from, string? name, IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc, string? subject, string? htmlBody)
    {
        From = from;
        Name = name;
        To = to;
        CC = cc;
        BCC = bcc;
        Subject = subject;
        HtmlBody = htmlBody;
    }
}