using MediatR;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Galleries.Queries.AddGallery;

public class AddGalleryQuery : IRequest<BaseIdModel>
{
    public string Name { get; set; }
}