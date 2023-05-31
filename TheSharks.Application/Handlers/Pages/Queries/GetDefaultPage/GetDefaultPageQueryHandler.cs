using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Models.Pages;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Pages.Queries.GetDefaultPage;

public class GetDefaultPageQueryHandler : IRequestHandler<GetDefaultPageQuery, GetDefaultPageModel>
{
    private readonly IRepository<Page> _pageRepository;

    public GetDefaultPageQueryHandler(IRepository<Page> pageRepository)
    {
        _pageRepository = pageRepository;
    }

    public async Task<GetDefaultPageModel> Handle(GetDefaultPageQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _pageRepository.Find(x => x.IsDefaultPage);
            return new GetDefaultPageModel { Link = $"page/{result.Link}" };
        }
        catch (AppException)
        {
            return new GetDefaultPageModel { Link = "news-items" };
        }
    }
}