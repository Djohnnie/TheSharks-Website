using MediatR;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.NewsItems.Queries.RemoveNewsItem;

public class RemoveNewsItemQuery : BaseIdModel, IRequest<BaseIdModel>
{

}