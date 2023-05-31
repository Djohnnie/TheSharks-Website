using AutoMapper;
using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Pages;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Pages.Queries.GetAllPages;

public class GetAllPagesQueryHandler : IRequestHandler<GetAllPagesQuery, PageOverviewListModel>
{
    private readonly IRepository<Page> _pageRepository;
    private readonly IMapper _mapper;

    public GetAllPagesQueryHandler(IRepository<Page> pageRepository, IMapper mapper)
    {
        _pageRepository = pageRepository;
        _mapper = mapper;
    }

    public async Task<PageOverviewListModel> Handle(GetAllPagesQuery request, CancellationToken cancellationToken)
    {
        var result = await _pageRepository.GetAllOrderBy(x => x.NavBarPosition, x => x.NavBarSubPosition);

        return new PageOverviewListModel { Pages = _mapper.Map<List<PageOverviewModel>>(result) };
    }
}