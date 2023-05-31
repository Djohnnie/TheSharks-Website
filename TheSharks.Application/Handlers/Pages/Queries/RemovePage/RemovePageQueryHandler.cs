using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Pages.Queries.RemovePage;

public class RemovePageQueryHandler : IRequestHandler<RemovePageQuery, BaseIdModel>
{
    private readonly IRepository<Page> _pageRepository;
    private readonly IMemoryCache _memoryCache;

    public RemovePageQueryHandler(
        IRepository<Page> pageRepository,
        IMemoryCache memoryCache)
    {
        _pageRepository = pageRepository;
        _memoryCache = memoryCache;
    }

    public async Task<BaseIdModel> Handle(RemovePageQuery request, CancellationToken cancellationToken)
    {
        var allPages = await _pageRepository.GetAllOrderBy(x => x.NavBarPosition, x => x.NavBarSubPosition);

        var pageToDelete = allPages.Single(x => x.Id == request.Id);
        if (pageToDelete.NavBarSubPosition == 0)
        {
            foreach (var page in allPages.Where(x => x.NavBarPosition > pageToDelete.NavBarPosition))
            {
                page.NavBarPosition--;
            }
        }
        else
        {
            foreach (var page in allPages.Where(x => x.NavBarPosition == pageToDelete.NavBarPosition && x.NavBarSubPosition > pageToDelete.NavBarSubPosition))
            {
                page.NavBarSubPosition--;
            }
        }

        await _pageRepository.UpdateAll(allPages, x => x.NavBarPosition, x => x.NavBarSubPosition);

        _memoryCache.Remove(pageToDelete.Link);

        var result = await _pageRepository.Delete(request.Id);

        return new BaseIdModel { Id = result.Id };
    }
}