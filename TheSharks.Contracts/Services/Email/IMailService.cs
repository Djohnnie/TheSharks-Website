namespace TheSharks.Contracts.Services.Email;

public interface IMailService
{
    Task SendEmail(string senderEmail, string senderName, IEnumerable<string> recipients, string subject, string message);

    Task SendEmail(string recipient, string subject, string message);
}