using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Models.Pages;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Pages.Queries.GetDefaultMembersPage;

public class GetDefaultMembersPageQueryHandler : IRequestHandler<GetDefaultMembersPageQuery, GetDefaultPageModel>
{
    private readonly IRepository<Page> _pageRepository;

    public GetDefaultMembersPageQueryHandler(IRepository<Page> pageRepository)
    {
        _pageRepository = pageRepository;
    }

    public async Task<GetDefaultPageModel> Handle(GetDefaultMembersPageQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _pageRepository.Find(x => x.IsDefaultPageForMembers);
            return new GetDefaultPageModel { Link = $"page/{result.Link}" };
        }
        catch (AppException)
        {
            return new GetDefaultPageModel { Link = "news-items" };
        }
    }
}