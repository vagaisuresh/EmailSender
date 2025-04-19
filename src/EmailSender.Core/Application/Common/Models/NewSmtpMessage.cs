namespace EmailSender.Core.Application.Common.Models;

public class NewSmtpMessage
{
    public string? From { get; set; }
    public string? Name { get; set; }

    public string? To { get; set; }
    public string? CC { get; set; }
    public string? BCC { get; set; }

    public string? Subject { get; set; }
    public string? Content { get; set; }

    public IFormFileCollection? Attachments { get; set; }

    public NewSmtpMessage(string from, string name, string? to, string cc, string bcc, string subject, string content, IFormFileCollection? attachments)
    {
        From = from;
        Name = name;
        To = to;
        CC = cc;
        BCC = bcc;
        Subject = subject;
        Content = content;
        Attachments = attachments;
    }
}