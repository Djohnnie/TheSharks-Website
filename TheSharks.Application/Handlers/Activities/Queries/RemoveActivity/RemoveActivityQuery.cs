using MediatR;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Activities.Queries.RemoveActivity;

public class RemoveActivityQuery : BaseIdModel, IRequest<BaseIdModel>
{
}