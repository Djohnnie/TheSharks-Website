using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.NewsItems.Queries.RemoveNewsItem;

public class RemoveNewsItemQueryHandler : IRequestHandler<RemoveNewsItemQuery, BaseIdModel>
{
    private readonly IRepository<NewsItem> _newsItemRepository;

    public RemoveNewsItemQueryHandler(IRepository<NewsItem> newsItemRepository)
    {
        _newsItemRepository = newsItemRepository;
    }

    public async Task<BaseIdModel> Handle(RemoveNewsItemQuery request, CancellationToken cancellationToken)
    {
        var deleteResult = await _newsItemRepository.Delete(request.Id); ;
        return new BaseIdModel { Id = deleteResult.Id };
    }
}