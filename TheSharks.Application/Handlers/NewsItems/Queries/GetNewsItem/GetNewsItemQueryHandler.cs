using AutoMapper;
using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.NewsItems;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.NewsItems.Queries.GetNewsItem;

public class GetNewsItemQueryHandler : IRequestHandler<GetNewsItemQuery, GetNewsItemModel>
{
    private readonly IRepository<NewsItem> _newsItemRepository;
    private readonly IMapper _mapper;

    public GetNewsItemQueryHandler(IRepository<NewsItem> newsItemRepository, IMapper mapper)
    {
        _newsItemRepository = newsItemRepository;
        _mapper = mapper;
    }

    public async Task<GetNewsItemModel> Handle(GetNewsItemQuery request, CancellationToken cancellationToken)
    {
        var result = await _newsItemRepository.Find(x => x.Id.Equals(request.Id), x => x.Author);
        return _mapper.Map<GetNewsItemModel>(result);
    }
}