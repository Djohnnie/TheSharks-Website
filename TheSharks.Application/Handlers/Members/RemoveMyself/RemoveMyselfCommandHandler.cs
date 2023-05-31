using MediatR;
using Microsoft.AspNetCore.Http;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Members.RemoveMyself;

public class RemoveMyselfCommandHandler : IRequestHandler<RemoveMyselfCommand>
{
    private readonly IMemberService<Member> _memberService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RemoveMyselfCommandHandler(
        IMemberService<Member> memberService,
        IHttpContextAccessor httpContextAccessor)
    {
        _memberService = memberService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(RemoveMyselfCommand request, CancellationToken cancellationToken)
    {
        var authenticatedUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "sid")?.Value);

        if (authenticatedUserId != request.Id)
        {
            throw new AppException("Je probeert iemand anders dan jezelf te verwijderen!");
        }

        await _memberService.Deactive(authenticatedUserId);

        return Unit.Value;
    }
}