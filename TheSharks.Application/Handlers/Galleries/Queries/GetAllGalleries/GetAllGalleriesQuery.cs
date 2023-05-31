using MediatR;
using TheSharks.Contracts.Models.Galleries;
using TheSharks.Contracts.Models.Pagination;

namespace TheSharks.Application.Handlers.Galleries.Queries.GetAllGalleries;

public class GetAllGalleriesQuery : PaginationBaseModel, IRequest<GalleryOverviewListModel>
{
}