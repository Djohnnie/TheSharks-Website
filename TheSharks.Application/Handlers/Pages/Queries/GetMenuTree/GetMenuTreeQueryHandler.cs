using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Pages;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Pages.Queries.GetMenuTree;

public class GetMenuTreeQueryHandler : IRequestHandler<GetMenuTreeQuery, MenuTreeModel>
{
    private readonly IRepository<Page> _pageRepository;

    public GetMenuTreeQueryHandler(IRepository<Page> pageRepository)
    {
        _pageRepository = pageRepository;
    }

    public async Task<MenuTreeModel> Handle(GetMenuTreeQuery request, CancellationToken cancellationToken)
    {
        var pages = await _pageRepository.GetAllOrderBy(x => x.NavBarPosition, x => x.NavBarSubPosition);

        var tree = new List<MenuNode>();

        foreach (var group in pages.GroupBy(x => x.NavBarPosition))
        {
            var children = new List<MenuNode>();
            foreach (var item in group.Skip(1))
            {
                children.Add(new MenuNode
                {
                    Title = item.Title,
                    Link = item.Link,
                    MembersOnly = item.IsOnlyAvailableForMembers,
                    NavBarPosition = item.NavBarPosition,
                    NavBarSubPosition = item.NavBarSubPosition,
                });
            }

            var branch = new MenuNode
            {
                Title = group.First().Title,
                Link = group.First().Link,
                MembersOnly = group.First().IsOnlyAvailableForMembers,
                NavBarPosition = group.First().NavBarPosition,
                NavBarSubPosition = group.First().NavBarSubPosition,
            };

            if (children.Count > 0) branch.Children = children;

            tree.Add(branch);

        }

        return new MenuTreeModel { Tree = tree };
    }
}