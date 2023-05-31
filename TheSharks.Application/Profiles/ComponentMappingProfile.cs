using AutoMapper;
using TheSharks.Contracts.Models.Pages;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Profiles;

public class ComponentMappingProfile : Profile
{
    public ComponentMappingProfile()
    {
        CreateMap<AddComponentModel, Component>();
        CreateMap<Component, GetComponentModel>();
    }
}