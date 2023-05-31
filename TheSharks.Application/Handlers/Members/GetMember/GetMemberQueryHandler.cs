using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using TheSharks.Contracts.Helpers;
using TheSharks.Contracts.Models.Members;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Common;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Members.GetMember;

public class GetMemberQueryHandler : IRequestHandler<GetMemberQuery, GetMemberModel>
{
    private readonly IMemberService<Member> _memberService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEncryptionHelper _encryptionHelper;
    private readonly IConfiguration _configuration;

    public GetMemberQueryHandler(
        IMemberService<Member> memberService,
        IHttpContextAccessor httpContextAccessor,
        IEncryptionHelper encryptionHelper,
        IConfiguration configuration)
    {
        _memberService = memberService;
        _httpContextAccessor = httpContextAccessor;
        _encryptionHelper = encryptionHelper;
        _configuration = configuration;
    }

    public async Task<GetMemberModel> Handle(GetMemberQuery request, CancellationToken cancellationToken)
    {
        var authenticatedUserId = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "sid")?.Value;

        var result = await _memberService.Find(request.Id);

        var extraInfo = GetExtraInfo(result.Bio);

        var output = new GetMemberModel()
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            PhoneNumber = authenticatedUserId == $"{request.Id}" ? extraInfo.PhoneNumber : string.Empty,
            Bio = extraInfo.Bio,
            ProfilePicture = result.ProfilePicture,
            UserName = result.UserName
        };

        return output;
    }

    private MemberProfileExtraInfo GetExtraInfo(string bio)
    {
        try
        {
            var secureKey = _configuration.GetValue<string>("SecureKey");
            var decryptedBio = _encryptionHelper.DecryptString(bio, secureKey);
            return JsonSerializer.Deserialize<MemberProfileExtraInfo>(decryptedBio);
        }
        catch
        {
            return new MemberProfileExtraInfo();
        }
    }
}