using MediatR;
using TheSharks.Contracts.Models.NewsItems;
using TheSharks.Contracts.Models.Pagination;

namespace TheSharks.Application.Handlers.NewsItems.Queries.GetNewsItemsOfMember;

public class GetNewsItemsOfMemberQuery : PaginationBaseModel, IRequest<GetNewsItemsModel>
{
    public Guid Id { get; set; }
}