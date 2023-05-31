using MediatR;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.NewsItems.Queries.UpdateNewsItem;

public class UpdateNewsItemQuery : IRequest<BaseIdModel>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime OriginalPublishDate { get; set; }
    public bool MembersOnly { get; set; }
}