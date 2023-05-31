using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.NewsItems.Queries.UpdateNewsItem;

public class UpdateNewsItemQueryHandler : IRequestHandler<UpdateNewsItemQuery, BaseIdModel>
{
    private readonly IRepository<NewsItem> _newsItemRepository;

    public UpdateNewsItemQueryHandler(IRepository<NewsItem> newsItemRepository)
    {
        _newsItemRepository = newsItemRepository;
    }

    public async Task<BaseIdModel> Handle(UpdateNewsItemQuery request, CancellationToken cancellationToken)
    {
        var result = await _newsItemRepository.Update(new NewsItem
        {
            Id = request.Id,
            Content = request.Content,
            Title = request.Title,
            PublishDate = request.OriginalPublishDate,
            MembersOnly = request.MembersOnly
        },
        x => x.Content,
        x => x.Title,
        x => x.PublishDate,
        x => x.MembersOnly);

        return new BaseIdModel { Id = result.Id };
    }
}