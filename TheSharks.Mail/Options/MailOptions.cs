namespace TheSharks.Mail.Options;

public class MailOptions
{
    public const string Mail = "Mail";

    public string SendGridKey { get; set; }
    public string Sender { get; set; }
}