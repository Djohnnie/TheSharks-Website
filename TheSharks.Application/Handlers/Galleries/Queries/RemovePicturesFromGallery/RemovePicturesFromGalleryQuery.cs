using MediatR;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Models.Galleries;

namespace TheSharks.Application.Handlers.Galleries.Queries.RemovePicturesFromGallery;

public class RemovePicturesFromGalleryQuery : BaseIdModel, IRequest<BaseIdModel>
{
    public IEnumerable<RemovePictureModel> Pictures { get; set; }
}