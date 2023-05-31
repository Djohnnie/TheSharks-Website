using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TheSharks.Contracts.DataAccess;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Pages.Queries.UpdateMenuTree;

public class UpdateMenuTreeQueryHandler : IRequestHandler<UpdateMenuTreeQuery, string>
{
    private readonly IRepository<Page> _repository;
    private readonly IMemoryCache _memoryCache;

    public UpdateMenuTreeQueryHandler(
        IRepository<Page> repository,
        IMemoryCache memoryCache)
    {
        _repository = repository;
        _memoryCache = memoryCache;
    }

    public async Task<string> Handle(UpdateMenuTreeQuery request, CancellationToken cancellationToken)
    {
        var pages = await _repository.GetAll(x => request.Pages.Select(p => p.Id).Contains(x.Id));

        foreach (var page in pages)
        {
            _memoryCache.Remove(page.Link);

            var pageInRequest = request.Pages.SingleOrDefault(x => x.Id == page.Id);
            if (pageInRequest != null)
            {
                page.NavBarPosition = pageInRequest.NavBarPosition;
                page.NavBarSubPosition = pageInRequest.NavBarSubPosition;
            }
        }

        var numberUpdated = await _repository.UpdateAll(pages, x => x.NavBarPosition, x => x.NavBarSubPosition);

        return numberUpdated + " items gewijzigd";
    }
}