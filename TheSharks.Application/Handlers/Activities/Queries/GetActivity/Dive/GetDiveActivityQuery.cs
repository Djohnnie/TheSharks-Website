using MediatR;
using TheSharks.Contracts.Models.Activities;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Activities.Queries.GetActivity.Dive;

public class GetDiveActivityQuery : BaseIdModel, IRequest<DiveActivityModel>
{
}