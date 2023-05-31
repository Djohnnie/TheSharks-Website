using MediatR;
using TheSharks.Contracts.Models.NewsItems;
using TheSharks.Contracts.Models.Pagination;

namespace TheSharks.Application.Handlers.NewsItems.Queries.GetAllNewsItems;

public class GetAllNewsItemsQuery : PaginationBaseModel, IRequest<GetNewsItemsModel>
{

}