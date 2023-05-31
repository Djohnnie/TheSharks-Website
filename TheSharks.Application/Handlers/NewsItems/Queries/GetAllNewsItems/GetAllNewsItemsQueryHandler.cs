using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.NewsItems;
using TheSharks.Contracts.Services.Statistics;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.NewsItems.Queries.GetAllNewsItems;

public class GetAllNewsItemsQueryHandler : IRequestHandler<GetAllNewsItemsQuery, GetNewsItemsModel>
{
    private readonly IRepository<NewsItem> _newsItemRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStatisticsService _statisticsService;
    private readonly IMapper _mapper;

    public GetAllNewsItemsQueryHandler(
        IRepository<NewsItem> newsItemRepository,
        IHttpContextAccessor httpContextAccessor,
        IStatisticsService statisticsService, IMapper mapper)
    {
        _newsItemRepository = newsItemRepository;
        _httpContextAccessor = httpContextAccessor;
        _statisticsService = statisticsService;
        _mapper = mapper;
    }

    public async Task<GetNewsItemsModel> Handle(GetAllNewsItemsQuery request, CancellationToken cancellationToken)
    {
        var isLoggedIn = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        var result = await _newsItemRepository.GetAllOrderBy(x => isLoggedIn || !x.MembersOnly, x => x.Author, x => x.PublishDate, request.Page, request.RecordsPerPage);
        var totalRecords = await _newsItemRepository.TableCount(x => isLoggedIn || !x.MembersOnly);

        await _statisticsService.RecordStatistics("news");

        return new GetNewsItemsModel { NewsItems = _mapper.Map<IEnumerable<GetNewsItemModel>>(result), TotalRecords = totalRecords };
    }
}