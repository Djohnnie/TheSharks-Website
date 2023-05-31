using AutoMapper;
using TheSharks.Contracts.Models.Identity.Roles;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Profiles;

public class RoleMappingProfile : Profile
{
    public RoleMappingProfile()
    {
        CreateMap<Role, RoleBaseModel>();
        CreateMap<Role, MemberRoleOverviewModel>();
        CreateMap<Role, RoleWithMembersCountModel>();
        CreateMap<Role, DiveCertificateModel>();
    }
}