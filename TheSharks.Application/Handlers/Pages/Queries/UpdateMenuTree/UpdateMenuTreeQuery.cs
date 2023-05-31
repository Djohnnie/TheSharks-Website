using MediatR;
using TheSharks.Contracts.Models.Pages;

namespace TheSharks.Application.Handlers.Pages.Queries.UpdateMenuTree;

public class UpdateMenuTreeQuery : IRequest<string>
{
    public IEnumerable<UpdateMenuTreeModel> Pages { get; set; }
}