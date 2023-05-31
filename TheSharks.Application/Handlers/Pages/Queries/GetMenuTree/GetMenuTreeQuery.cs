using MediatR;
using TheSharks.Contracts.Models.Pages;

namespace TheSharks.Application.Handlers.Pages.Queries.GetMenuTree;

public class GetMenuTreeQuery : IRequest<MenuTreeModel>
{
}