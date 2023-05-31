using System.Linq.Expressions;
using System.Transactions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Pages.Queries.UpdatePage;

public class UpdatePageQueryHandler : IRequestHandler<UpdatePageQuery, BaseIdModel>
{
    private readonly IRepository<Page> _pageRepository;
    private readonly IRepository<Component> _componentRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;

    public UpdatePageQueryHandler(
        IRepository<Page> pageRepository,
        IRepository<Component> componentRepository,
        IMemoryCache memoryCache, IMapper mapper)
    {
        _pageRepository = pageRepository;
        _componentRepository = componentRepository;
        _memoryCache = memoryCache;
        _mapper = mapper;
    }

    public async Task<BaseIdModel> Handle(UpdatePageQuery request, CancellationToken cancellationToken)
    {
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var pagesToUpdate = new List<Page>();

        if (request.IsDefaultPage)
        {
            await UpdatePageByAction(
                p => p.IsDefaultPage, p => p.IsDefaultPage = false, pagesToUpdate);
        }

        if (request.IsDefaultPageForMembers)
        {
            await UpdatePageByAction(
                p => p.IsDefaultPageForMembers, p => p.IsDefaultPageForMembers = false, pagesToUpdate);
        }

        if (pagesToUpdate.Count > 0)
        {
            await _pageRepository.UpdateAll(pagesToUpdate);

            foreach (var page in pagesToUpdate)
            {
                _memoryCache.Remove(page.Link);
            }
        }

        await _componentRepository.Delete(x => x.Page.Id == request.Id);

        var pageToUpdate = await _pageRepository.Find(x => x.Id == request.Id);
        pageToUpdate.Title = request.Title;
        pageToUpdate.IsOnlyAvailableForMembers = request.IsOnlyAvailableForMembers;
        pageToUpdate.IsDefaultPage = request.IsDefaultPage;
        pageToUpdate.IsDefaultPageForMembers = request.IsDefaultPageForMembers;
        pageToUpdate.Components = _mapper.Map<List<Component>>(request.Components);
        pageToUpdate.Link = request.Link;
        pageToUpdate.NavBarPosition = request.NavBarPosition;
        pageToUpdate.NavBarSubPosition = request.NavBarSubPosition;

        var result = await _pageRepository.Update(pageToUpdate);

        _memoryCache.Remove(request.Link);

        transactionScope.Complete();

        return new BaseIdModel { Id = result.Id };
    }

    private async Task UpdatePageByAction(Expression<Func<Page, bool>> predicate, Action<Page> updateAction, List<Page> pagesToUpdate)
    {
        var pages = await _pageRepository.GetAll(predicate);
        foreach (var page in pages)
        {
            var pageToUpdate = pagesToUpdate.SingleOrDefault(x => x.Id == page.Id);
            if (pageToUpdate == null)
            {
                updateAction(page);
                page.IsDefaultPage = false;
                pagesToUpdate.Add(page);
            }
            else
            {
                updateAction(pageToUpdate);
            }
        }
    }
}