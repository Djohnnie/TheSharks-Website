using MediatR;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Models.Galleries;

namespace TheSharks.Application.Handlers.Galleries.Queries.GetGallery;

public class GetGalleryQuery : BaseIdModel, IRequest<GetGalleryModel>
{

}