using MediatR;
using TheSharks.Contracts.Models.Members;
using TheSharks.Contracts.Services.Members;
using TheSharks.Contracts.Services.Statistics;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Members.ResetPassword;

public class ResetPasswordQueryHandler : IRequestHandler<ResetPasswordQuery, Member>
{
    private readonly IMemberService<Member> _memberService;
    private readonly IStatisticsService _statisticsService;

    public ResetPasswordQueryHandler(
        IMemberService<Member> memberService,
        IStatisticsService statisticsService)
    {
        _memberService = memberService;
        _statisticsService = statisticsService;
    }

    public async Task<Member> Handle(ResetPasswordQuery request, CancellationToken cancellationToken)
    {
        var result = await _memberService.ResetPassword(new ResetPasswordModel
        {
            Id = request.Id,
            NewPassword = request.NewPassword,
            Token = request.Token
        });

        await _statisticsService.RecordStatistics("reset");
        
        return result;
    }
}