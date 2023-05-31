namespace TheSharks.Domain.Entities;

public class LogEntry
{
    public Guid Id { get; set; }
    public long SysId { get; set; }
    public DateTimeOffset Date { get; set; }
    public string? User { get; set; }
    public string? Identifier { get; set; }
    public string? Message { get; set; }
    public string? Data { get; set; }
}

public enum LogIdentifier
{
    Login,
    LoginSuccessful,
    LoginFailed,
    ForgotPassword,
    ResetPassword,
    SuccessfulPasswordReset,
    UnsuccessfulPasswordReset,
}