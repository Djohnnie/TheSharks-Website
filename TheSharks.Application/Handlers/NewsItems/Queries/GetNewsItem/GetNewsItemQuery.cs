using MediatR;
using TheSharks.Contracts.Models.NewsItems;

namespace TheSharks.Application.Handlers.NewsItems.Queries.GetNewsItem;

public class GetNewsItemQuery : IRequest<GetNewsItemModel>
{
    public Guid Id { get; set; }
}