using MediatR;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Galleries.Queries.RemoveGallery;

public class RemoveGalleryQuery : BaseIdModel, IRequest<BaseIdModel>
{
}