using MediatR;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.NewsItems.Queries.AddNewsItem;

public class AddNewsItemQuery : IRequest<BaseIdModel>
{
    public Guid Author { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool MembersOnly { get; set; }
}