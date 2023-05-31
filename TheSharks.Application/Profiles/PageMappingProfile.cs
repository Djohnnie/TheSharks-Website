using AutoMapper;
using TheSharks.Contracts.Models.Pages;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Profiles;

public class PageMappingProfile : Profile
{
    public PageMappingProfile()
    {
        CreateMap<Page, GetPageModel>();
        CreateMap<Page, PageOverviewModel>();
    }
}