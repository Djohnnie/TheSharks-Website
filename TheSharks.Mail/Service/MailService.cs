using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Services.Email;
using TheSharks.Mail.Options;

namespace TheSharks.Mail.Service;

public class MailService : IMailService
{
    private readonly MailOptions _mailOptions;

    public MailService(IOptions<MailOptions> options)
    {
        _mailOptions = options.Value;
    }

    public async Task SendEmail(string senderEmail, string senderName, IEnumerable<string> recipients, string subject, string message)
    {
        var client = new SendGridClient(_mailOptions.SendGridKey);
        var mail = new SendGridMessage
        {
            ReplyTo = new EmailAddress(senderEmail, senderName),
            From = new EmailAddress(_mailOptions.Sender, $"{senderName} via The Sharks"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };

        mail.AddTo(senderEmail);

        foreach (var recipient in recipients)
        {
            if (recipient != senderEmail)
            {
                mail.AddBcc(recipient);
            }
        }

        var response = await client.SendEmailAsync(mail);
        if (response.StatusCode != System.Net.HttpStatusCode.Accepted) throw new AppException("Fout bij het versturen van email(s).");
    }

    public async Task SendEmail(string recipient, string subject, string message)
    {
        var client = new SendGridClient(_mailOptions.SendGridKey);
        var mail = new SendGridMessage
        {
            From = new EmailAddress(_mailOptions.Sender, "The Sharks"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        mail.AddTo(recipient);
        var response = await client.SendEmailAsync(mail);
        if (response.StatusCode != System.Net.HttpStatusCode.Accepted) throw new AppException("Fout bij het versturen van email(s).");
    }
}