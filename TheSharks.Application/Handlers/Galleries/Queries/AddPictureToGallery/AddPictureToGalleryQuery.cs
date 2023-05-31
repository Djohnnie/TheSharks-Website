using MediatR;
using Microsoft.AspNetCore.Http;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Galleries.Queries.AddPictureToGallery;

public class AddPictureToGalleryQuery : BaseIdModel, IRequest<BaseIdModel>
{
    public IEnumerable<IFormFile> Pictures { get; set; }
}