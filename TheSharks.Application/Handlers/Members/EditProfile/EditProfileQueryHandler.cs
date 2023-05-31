using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using TheSharks.Contracts.Helpers;
using TheSharks.Contracts.Models.Members;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Common;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Members.EditProfile;

public class EditProfileQueryHandler : IRequestHandler<EditProfileQuery, Member>
{
    private readonly IMemberService<Member> _memberService;
    private readonly IEncryptionHelper _encryptionHelper;
    private readonly IMemoryCache _memoryCache;
    private readonly IConfiguration _configuration;

    public EditProfileQueryHandler(
        IMemberService<Member> memberService,
        IEncryptionHelper encryptionHelper,
        IMemoryCache memoryCache,
        IConfiguration configuration)
    {
        _memberService = memberService;
        _encryptionHelper = encryptionHelper;
        _memoryCache = memoryCache;
        _configuration = configuration;
    }

    public async Task<Member> Handle(EditProfileQuery request, CancellationToken cancellationToken)
    {
        var secureKey = _configuration.GetValue<string>("SecureKey");

        var currentMember = await _memberService.Find(request.Id);

        var profileExtraInfo = new MemberProfileExtraInfo
        {
            PhoneNumber = request.PhoneNumber,
            Bio = request.Bio
        };

        var result = await _memberService.Update(new EditProfileModel
        {
            FirstName = currentMember.FirstName,
            LastName = currentMember.LastName,
            Id = request.Id,
            ProfilePicture = request.ProfilePicture,
            Bio = _encryptionHelper.EncryptString(JsonSerializer.Serialize(profileExtraInfo), secureKey),
        });

        _memoryCache.Remove($"PROFILE_PICTURE_{request.Id}");

        return result;
    }
}