using MediatR;
using TheSharks.Contracts.Models.Identity.Authentication;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Contracts.Services.Statistics;

namespace TheSharks.Application.Handlers.Identity.Queries.Authentication.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, TokenResponse>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IStatisticsService _statisticsService;

    public LoginQueryHandler(
        IAuthenticationService authenticationService,
        IStatisticsService statisticsService)
    {
        _authenticationService = authenticationService;
        _statisticsService = statisticsService;
    }

    public async Task<TokenResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.Login(
            new LoginModel
            {
                Username = request.Username,
                Password = request.Password,
                Persist = request.Persist
            });

        await _statisticsService.RecordStatistics("login");

        return result;
    }
}