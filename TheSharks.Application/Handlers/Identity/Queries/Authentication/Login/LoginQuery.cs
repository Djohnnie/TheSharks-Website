using MediatR;
using TheSharks.Contracts.Models.Identity.Authentication;

namespace TheSharks.Application.Handlers.Identity.Queries.Authentication.Login;

public class LoginQuery : IRequest<TokenResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool Persist { get; set; }
}