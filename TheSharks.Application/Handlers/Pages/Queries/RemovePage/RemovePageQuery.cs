using MediatR;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Pages.Queries.RemovePage;

public class RemovePageQuery : BaseIdModel, IRequest<BaseIdModel>
{
}