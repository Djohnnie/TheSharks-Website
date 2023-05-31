using TheSharks.Contracts.Models.Identity.Authentication;
using TheSharks.Domain.Entities;

namespace TheSharks.Contracts.Services.Identity;

public interface IAuthenticationService
{
    Task<TokenResponse> Login(LoginModel model);
    Task<Member> Register(RegisterModel model);
    Task<Member> ForgotPassword(ForgotPasswordModel model);
}