namespace TheSharks.Contracts.Models.Identity.Authentication;

public class TokenResponse
{
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
}