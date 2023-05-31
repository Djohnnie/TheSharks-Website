using AutoMapper;
using TheSharks.Contracts.Models.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Profiles;

public class MemberMappingProfile : Profile
{
    public MemberMappingProfile()
    {
        CreateMap<Member, MemberOverviewModel>();
        CreateMap<Member, MemberNameModel>();
    }
}