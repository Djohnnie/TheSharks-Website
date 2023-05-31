using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using TheSharks.Contracts;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Models.Identity.Authentication;
using TheSharks.Contracts.Services.Email;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Contracts.Services.Logging;
using TheSharks.Domain.Entities;

namespace TheSharks.Identity.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<Member> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IMailService _mailService;
    private readonly IRoleService<Role> _roleService;
    private readonly ILogService _logService;

    public AuthenticationService(
        UserManager<Member> userManager,
        IConfiguration configuration,
        IMailService mailservice,
        IRoleService<Role> roleService,
        ILogService logService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _mailService = mailservice;
        _roleService = roleService;
        _logService = logService;
    }

    public async Task<TokenResponse> Login(LoginModel model)
    {
        await _logService.RecordLog(LogIdentifier.Login, model.Username, $"Login attempt for {model.Username}");

        var member = await _userManager.FindByNameAsync(model.Username);

        if (member == null && model.Username.Contains("@"))
        {
            member = await _userManager.FindByEmailAsync(model.Username);
        }

        if (member != null && await _userManager.CheckPasswordAsync(member, model.Password))
        {
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sid, $"{member.Id}"),
                new Claim(JwtRegisteredClaimNames.Name, member.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, member.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["JWT:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["JWT:Audience"])
            };

            authClaims.AddRange(await ExtractRoleClaimsFromUser(member.Id));

            var token = GetToken(authClaims, model.Persist);

            await _logService.RecordLog(LogIdentifier.LoginSuccessful, model.Username, $"Login successful for {model.Username}");

            return new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = token.ValidTo
            };
        }

        await _logService.RecordLog(LogIdentifier.LoginFailed, model.Username, $"Login attempt failed for {model.Username}");

        throw new IdentityException("Gebruikersnaam of wachtwoord onjuist");
    }

    private async Task<IEnumerable<Claim>> ExtractRoleClaimsFromUser(Guid id)
    {
        var toReturnClaims = new List<Claim>();

        foreach (var userRole in await _roleService.GetRolesAssignedToMember(id))
        {
            toReturnClaims.AddRange(await _roleService.GetClaimsOfRole(userRole));

        }

        return toReturnClaims;
    }

    public async Task<Member> Register(RegisterModel model)
    {
        var userExists = await _userManager.FindByNameAsync(model.UserName);
        if (userExists != null) throw new IdentityException("Gebruikersnaam is al in gebruik!");

        Member member = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.UserName,
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        var result = await _userManager.CreateAsync(member, await GeneratePassword(member.UserName));
        if (!result.Succeeded) throw new IdentityException("Er is iets fout gegaan tijdens het aanmaken van de registratie!");

        var link = await GenerateResetTokenAndLink(member, "reset-password");
        await _mailService.SendEmail(
            member.Email,
            StringConstants.NEW_ACCOUNT_EMAIL_SUBJECT,
            string.Format(StringConstants.NEW_ACCOUNT_EMAIL_BODY, member.FirstName, member.UserName, link));

        return member;
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims, bool persist)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            expires: persist ? DateTime.Now.AddDays(365) : DateTime.Now.AddHours(24),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

    public async Task<Member> ForgotPassword(ForgotPasswordModel model)
    {
        await _logService.RecordLog(LogIdentifier.ForgotPassword, model.Email, $"Forgot password attempt for {model.Email}");

        var member = await _userManager.FindByEmailAsync(model.Email);
        if (member == null) throw new IdentityException("Het email adres werd niet herkent");

        var link = await GenerateResetTokenAndLink(member, "reset-password");

        await _mailService.SendEmail(
            member.Email,
            StringConstants.PASSWORD_RESET_REQUEST_EMAIL_SUBJECT,
            string.Format(StringConstants.PASSWORD_RESET_REQUEST_EMAIL_BODY, member.FirstName, link, member.UserName));

        return member;
    }

    private async Task<string> GenerateResetTokenAndLink(Member member, string endpoint)
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(member);

        var base_url = _configuration["Mail:FrontendLink"] + endpoint;
        var link = base_url + "?Id=" + member.Id + "&token=" + HttpUtility.UrlEncode(token);

        return link;
    }

    private async Task<string> GeneratePassword(string username)
    {
        return username + Guid.NewGuid().ToString("N").Substring(1, 10);
    }
}