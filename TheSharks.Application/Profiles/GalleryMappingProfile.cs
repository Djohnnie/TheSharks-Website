using AutoMapper;
using TheSharks.Contracts.Models.Galleries;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Profiles;

public class GalleryMappingProfile : Profile
{
    public GalleryMappingProfile()
    {
        CreateMap<Gallery, GetGalleryModel>();
        CreateMap<Picture, GetGalleryPictureModel>();
        CreateMap<Gallery, GalleryOverviewModel>();
    }
}