using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Pages;
using TheSharks.Contracts.Services.Statistics;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Pages.Queries.GetPage;

public class GetPageQueryHandler : IRequestHandler<GetPageQuery, GetPageModel>
{
    private readonly IRepository<Page> _pageRepository;
    private readonly IStatisticsService _statisticsService;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;

    public GetPageQueryHandler(
        IRepository<Page> pageRepository,
        IStatisticsService statisticsService,
        IMemoryCache memoryCache, IMapper mapper)
    {
        _pageRepository = pageRepository;
        _statisticsService = statisticsService;
        _memoryCache = memoryCache;
        _mapper = mapper;
    }

    public async Task<GetPageModel> Handle(GetPageQuery request, CancellationToken cancellationToken)
    {
        var toReturn = _memoryCache.Get<GetPageModel>(request.Link);

        if (toReturn == null)
        {
            var result = await _pageRepository.Find(x => x.Link == request.Link, x => x.Components);
            toReturn = _mapper.Map<GetPageModel>(result);
            _memoryCache.Set(request.Link, toReturn, TimeSpan.FromDays(100));
        }

        await _statisticsService.RecordStatistics(request.Link);

        return toReturn;
    }
}