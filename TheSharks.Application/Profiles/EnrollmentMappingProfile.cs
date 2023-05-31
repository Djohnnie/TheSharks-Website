using AutoMapper;
using TheSharks.Contracts.Models.Activities.BaseModels;
using TheSharks.Contracts.Models.Common;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Profiles;

public class EnrollmentMappingProfile : Profile
{
    public EnrollmentMappingProfile()
    {
        CreateMap<MemberEnrollment, BaseIdModel>();
        CreateMap<GuestEnrollment, BaseIdModel>();
        CreateMap<GuestEnrollment, ActivityEnrollmentModel>()
            .ForMember(dest => dest.DiveCertificate, opt => opt.MapFrom(src => src.DiveLevel));
    }
}