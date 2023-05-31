using AutoMapper;
using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Pages.Queries.AddPage;

public class AddPageQueryHandler : IRequestHandler<AddPageQuery, BaseIdModel>
{
    private readonly IRepository<Page> _pageRepository;
    private readonly IMapper _mapper;

    public AddPageQueryHandler(IRepository<Page> pageRepository, IMapper mapper)
    {
        _pageRepository = pageRepository;
        _mapper = mapper;
    }

    public async Task<BaseIdModel> Handle(AddPageQuery request, CancellationToken cancellationToken)
    {
        var allPages = await _pageRepository.GetAllOrderBy(x => x.NavBarPosition, x => x.NavBarSubPosition);
        var lastPage = allPages.MaxBy(x => x.NavBarPosition);
        var currentLastNavBarPosition = lastPage?.NavBarPosition ?? 0;

        var result = await _pageRepository.Add(new Page
        {
            Title = request.Title,
            Link = request.Link ?? request.Title.ToLowerInvariant(),
            Components = _mapper.Map<List<Component>>(request.Components),
            NavBarPosition = currentLastNavBarPosition + 1,
            NavBarSubPosition = 0
        });

        return new BaseIdModel { Id = result.Id };
    }
}