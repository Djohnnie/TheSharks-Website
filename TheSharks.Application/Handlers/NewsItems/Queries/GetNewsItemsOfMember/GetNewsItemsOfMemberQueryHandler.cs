using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.NewsItems;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.NewsItems.Queries.GetNewsItemsOfMember;

public class GetNewsItemsOfMemberQueryHandler : IRequestHandler<GetNewsItemsOfMemberQuery, GetNewsItemsModel>
{
    private readonly IRepository<NewsItem> _newsItemRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public GetNewsItemsOfMemberQueryHandler(
        IRepository<NewsItem> newsItemRepository,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper)
    {
        _newsItemRepository = newsItemRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<GetNewsItemsModel> Handle(GetNewsItemsOfMemberQuery request, CancellationToken cancellationToken)
    {
        var isLoggedIn = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        var result = await _newsItemRepository.GetAllOrderBy(x => x.Author.Id.Equals(request.Id) && (isLoggedIn || !x.MembersOnly), x => x.Author, x => x.PublishDate, request.Page, request.RecordsPerPage);
        var totalRecords = await _newsItemRepository.TableCount(x => x.Author.Id.Equals(request.Id) && (isLoggedIn || !x.MembersOnly));

        return new GetNewsItemsModel { NewsItems = _mapper.Map<IEnumerable<GetNewsItemModel>>(result), TotalRecords = totalRecords };
    }
}