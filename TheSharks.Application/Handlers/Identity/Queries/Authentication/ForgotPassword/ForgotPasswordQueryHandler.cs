using MediatR;
using TheSharks.Contracts.Models.Identity.Authentication;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Identity.Queries.Authentication.ForgotPassword;

public class ForgotPasswordQueryHandler : IRequestHandler<ForgotPasswordQuery, Member>
{
    private readonly IAuthenticationService _authenticationService;

    public ForgotPasswordQueryHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<Member> Handle(ForgotPasswordQuery request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.ForgotPassword(new ForgotPasswordModel { Email = request.Email });
        return result;
    }
}