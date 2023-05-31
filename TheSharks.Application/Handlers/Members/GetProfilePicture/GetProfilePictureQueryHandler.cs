using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TheSharks.Contracts.Models.Members;
using TheSharks.Contracts.Models.Pages;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Members.GetProfilePicture;

public class GetProfilePictureQueryHandler : IRequestHandler<GetProfilePictureQuery, ProfilePictureModel>
{
    private readonly IMemberService<Member> _memberService;
    private readonly IMemoryCache _memoryCache;

    public GetProfilePictureQueryHandler(
        IMemberService<Member> memberService,
        IMemoryCache memoryCache)
    {
        _memberService = memberService;
        _memoryCache = memoryCache;
    }

    public async Task<ProfilePictureModel> Handle(GetProfilePictureQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"PROFILE_PICTURE_{request.Id}";
        var profilePicture = _memoryCache.Get<byte[]>(cacheKey);

        if (profilePicture == null)
        {
            var member = await _memberService.Find(request.Id);
            profilePicture = member.ProfilePicture;
            _memoryCache.Set(cacheKey, profilePicture, TimeSpan.FromDays(100));
        }

        return new ProfilePictureModel { Picture = profilePicture };
    }
}