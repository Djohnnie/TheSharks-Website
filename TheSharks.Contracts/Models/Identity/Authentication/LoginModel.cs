namespace TheSharks.Contracts.Models.Identity.Authentication;

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool Persist { get; set; }
}