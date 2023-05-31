using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Activities.Queries.RemoveActivity;

public class RemoveActivityQueryHandler : IRequestHandler<RemoveActivityQuery, BaseIdModel>
{
    private readonly IRepository<Activity> _activityRepository;

    public RemoveActivityQueryHandler(IRepository<Activity> activityRepository)
    {
        _activityRepository = activityRepository;
    }

    public async Task<BaseIdModel> Handle(RemoveActivityQuery request, CancellationToken cancellationToken)
    {
        var removeResult = await _activityRepository.Delete(request.Id);

        return new BaseIdModel { Id = removeResult.Id };
    }
}