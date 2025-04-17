namespace EmailSender.Core.Application.Common.Models;

public class NewMessage
{
    public IEnumerable<string>? To { get; set; }
    public IEnumerable<string>? CC { get; set; }
    public IEnumerable<string>? BCC { get; set; }

    public string Subject { get; set; }
    public string Body { get; set; }

    public NewMessage(IEnumerable<string>? to, IEnumerable<string>? cc, IEnumerable<string> bcc, string subject, string body)
    {
        To = to;
        CC = cc;
        BCC = bcc;
        Subject = subject;
        Body = body;
    }
}