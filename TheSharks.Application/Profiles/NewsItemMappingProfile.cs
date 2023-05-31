using AutoMapper;
using TheSharks.Contracts.Models.NewsItems;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Profiles;

public class NewsItemMappingProfile : Profile
{
    public NewsItemMappingProfile()
    {
        CreateMap<NewsItem, GetNewsItemModel>();
    }
}