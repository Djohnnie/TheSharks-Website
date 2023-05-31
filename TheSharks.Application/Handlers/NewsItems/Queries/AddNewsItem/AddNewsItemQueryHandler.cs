using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.NewsItems.Queries.AddNewsItem;

public class AddNewsItemQueryHandler : IRequestHandler<AddNewsItemQuery, BaseIdModel>
{
    private readonly IRepository<NewsItem> _newsItemRepository;
    private readonly IMemberService<Member> _memberService;

    public AddNewsItemQueryHandler(IRepository<NewsItem> newsItemRepository, IMemberService<Member> memberService)
    {
        _newsItemRepository = newsItemRepository;
        _memberService = memberService;
    }

    public async Task<BaseIdModel> Handle(AddNewsItemQuery request, CancellationToken cancellationToken)
    {
        var member = await _memberService.Find(request.Author);

        var result = await _newsItemRepository.Add(new NewsItem
        {
            Title = request.Title,
            Content = request.Content,
            PublishDate = DateTimeOffset.UtcNow,
            Author = member,
            MembersOnly = request.MembersOnly
        });

        return new BaseIdModel { Id = result.Id };
    }
}