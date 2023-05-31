namespace TheSharks.Contracts.Models.Members;

public class ResetPasswordModel
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set; }
}