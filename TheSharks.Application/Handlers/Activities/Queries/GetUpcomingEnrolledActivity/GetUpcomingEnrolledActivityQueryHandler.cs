using AutoMapper;
using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Activities;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Activities.Queries.GetUpcomingEnrolledActivity;

public class GetUpcomingEnrolledActivityQueryHandler : IRequestHandler<GetUpcomingEnrolledActivityQuery, ActivityOverviewModel>
{
    private readonly IMemberEnrollmentRepository<MemberEnrollment> _memberEnrollmentRepository;
    private readonly IMapper _mapper;

    public GetUpcomingEnrolledActivityQueryHandler(IMemberEnrollmentRepository<MemberEnrollment> memberEnrollmentRepository, IMapper mapper)
    {
        _memberEnrollmentRepository = memberEnrollmentRepository;
        _mapper = mapper;
    }

    public async Task<ActivityOverviewModel> Handle(GetUpcomingEnrolledActivityQuery request, CancellationToken cancellationToken)
    {
        var activity = await _memberEnrollmentRepository.GetUpcomingWithResponsible(request.Id);
        if (activity == null) return null;

        return _mapper.Map<ActivityOverviewModel>(activity.Activity);
    }
}