using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Models.Enrollments;
using TheSharks.Contracts.Services.Email;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Enrollments.Queries.RemoveEnrollment;

public class RemoveEnrollmentQueryHandler : IRequestHandler<RemoveEnrollmentQuery, BaseIdModel>
{
    private readonly IRepository<Enrollment> _enrollmentRepository;
    private readonly IMemberEnrollmentRepository<MemberEnrollment> _memberEnrollmentRepository;
    private readonly IMemberService<Member> _memberService;
    private readonly IRepository<Activity> _activityRepository;
    private readonly IMailService _mailService;

    public RemoveEnrollmentQueryHandler(
        IRepository<Enrollment> enrollmentRepository, 
        IMemberEnrollmentRepository<MemberEnrollment> memberEnrollmentRepository,
        IMemberService<Member> memberService,
        IRepository<Activity> activityRepository,
        IMailService mailService)
    {
        _enrollmentRepository = enrollmentRepository;
        _memberEnrollmentRepository = memberEnrollmentRepository;
        _memberService = memberService;
        _activityRepository = activityRepository;
        _mailService = mailService;
    }

    public async Task<BaseIdModel> Handle(RemoveEnrollmentQuery request, CancellationToken cancellationToken)
    {
        var activity = await _activityRepository.Find(x => x.Id == request.ActivityId, x => x.Responsible);
        var registrator = await _memberService.Find(request.MemberId);

        await _enrollmentRepository.Delete(x => x.Activity.Id == request.ActivityId && x.Registrator.Id == request.MemberId);

        await SendEmail(activity, registrator);

        return new BaseIdModel { Id = Guid.Empty };
    }

    private async Task SendEmail(Activity activity, Member registrator)
    {
        var bodyToResponsible = $"Beste {activity.Responsible.FirstName},<br><br><br>{registrator.FirstName} {registrator.LastName} heeft een eerdere inschrijving ongedaan gemaakt voor {activity.Name}.";
        
        bodyToResponsible += "<br><br><br>Met vriendelijke groeten,<br>The Sharks";
        await _mailService.SendEmail(activity.Responsible.Email, activity.Name, bodyToResponsible);

        var bodyToRegistrator = $"Beste {registrator.FirstName},<br><br><br>Je annulatie voor {activity.Name} werd doorgegeven aan {activity.Responsible.FirstName}.<br><br><br>Met vriendelijke groeten,<br>The Sharks";
        await _mailService.SendEmail(registrator.Email, activity.Name, bodyToRegistrator);
    }
}