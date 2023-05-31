using MediatR;
using TheSharks.Application.Handlers.Enrollments.Queries.AddEnrollment.Guests;
using TheSharks.Application.Handlers.Enrollments.Queries.AddEnrollment.Members;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Models.Enrollments;
using TheSharks.Contracts.Services.Email;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Enrollments.Queries.AddEnrollment.EnrollmentDelegator;

public class EnrollmentDelegatorQueryHandler : IRequestHandler<EnrollmentDelegatorQuery, IEnumerable<BaseIdModel>>
{
    private readonly IRepository<Activity> _activityRepository;
    private readonly IRepository<OpenWaterTest> _openWaterTestRepository;
    private readonly IMailService _mailService;
    private readonly IMemberService<Member> _memberService;
    private readonly IMediator _mediator;

    public EnrollmentDelegatorQueryHandler(
        IMemberService<Member> memberService,
        IRepository<Activity> activityRepository,
        IRepository<OpenWaterTest> openWaterTestRepository,
        IMailService mailService,
        IMediator mediator)
    {
        _activityRepository = activityRepository;
        _openWaterTestRepository = openWaterTestRepository;
        _mailService = mailService;
        _memberService = memberService;
        _mediator = mediator;
    }

    public async Task<IEnumerable<BaseIdModel>> Handle(EnrollmentDelegatorQuery request, CancellationToken cancellationToken)
    {
        var activity = await _activityRepository.Find(x => x.Id == request.ActivityId, x => x.Responsible);
        if (activity == null) return Enumerable.Empty<BaseIdModel>();

        var registrator = await _memberService.Find(request.RegistratorId);
        if (registrator == null) return Enumerable.Empty<BaseIdModel>();

        var members = await _mediator.Send(new AddMemberEnrollmentQuery { Activity = activity, Registrator = registrator, MemberEnrollments = request.MemberEnrollments });
        var guests = await _mediator.Send(new AddGuestEnrollmentQuery { Activity = activity, Registrator = registrator, GuestEnrollments = request.GuestEnrollments });

        var openWaterTests = await _openWaterTestRepository.GetAll();

        await SendEmail(activity, registrator, request.MemberEnrollments, request.GuestEnrollments, openWaterTests);

        return new List<BaseIdModel>().Concat(members).Concat(guests).ToList();
    }

    private async Task SendEmail(Activity activity, Member registrator, IEnumerable<EnrollmentMemberModel> members, IEnumerable<EnrollmentGuestModel> guests, IList<OpenWaterTest> openWaterTests)
    {
        var remarks = string.Empty;

        var actualOpenWaterTests = new List<OpenWaterTest>();

        var bodyToResponsible = $"Beste {activity.Responsible.FirstName},<br><br><br>{registrator.FirstName} {registrator.LastName} heeft een inschrijving gedaan voor {activity.Name}.";

        if (members.Any())
        {
            bodyToResponsible += "<br><br>De volgende leden werden ingeschreven:";
            bodyToResponsible += "<ul>";
            foreach (var member in members)
            {
                bodyToResponsible += $"<li><b>{member.Registree}</b> als {(member.AsDiver ? "duiker" : "niet duiker")}</li>";
                remarks = member.Remark;
            }
            bodyToResponsible += "</ul>";
        }

        if (guests.Any())
        {
            bodyToResponsible += "<br><br>De volgende gasten werden ingeschreven:";
            bodyToResponsible += "<ul>";
            foreach (var guest in guests)
            {
                bodyToResponsible += $"<li><b>{guest.Registree}</b> als {(guest.AsDiver ? $"duiker ({guest.DiveCertificate})" : "niet duiker")}</li>";
                remarks = guest.Remark;
            }
            bodyToResponsible += "</ul>";
        }

        if (!string.IsNullOrWhiteSpace(remarks))
        {
            bodyToResponsible += $"<br><br>{registrator.FirstName} heeft de volgende opmerking meegestuurd:";
            bodyToResponsible += $"<br><i>{remarks}</i>";
        }

        if (members.Any(x => x.OpenWaterTestId.HasValue) || guests.Any(x => x.OpenWaterTestId.HasValue))
        {
            bodyToResponsible += "<br><br>";
        }

        foreach (var member in members)
        {
            if (member.OpenWaterTestId.HasValue)
            {
                var openWaterTest = openWaterTests.FirstOrDefault(x => x.Id == member.OpenWaterTestId.Value);
                if (!actualOpenWaterTests.Contains(openWaterTest))
                {
                    actualOpenWaterTests.Add(openWaterTest);
                }

                bodyToResponsible += $"<br>Lid <b>{member.Registree}</b> wil graag openwaterproef '<i><b>{openWaterTest.Title}</b></i>' laten afnemen.";
            }
        }

        foreach (var guest in guests)
        {
            if (guest.OpenWaterTestId.HasValue)
            {
                var openWaterTest = openWaterTests.FirstOrDefault(x => x.Id == guest.OpenWaterTestId.Value);
                if (!actualOpenWaterTests.Contains(openWaterTest))
                {
                    actualOpenWaterTests.Add(openWaterTest);
                }

                bodyToResponsible += $"<br>Gast <b>{guest.Registree}</b> wil graag openwaterproef '<i><b>{openWaterTest.Title}</b></i>' laten afnemen.";
            }
        }

        bodyToResponsible += "<br><br><br>Met vriendelijke groeten,<br>The Sharks";

        if (actualOpenWaterTests.Any())
        {
            bodyToResponsible += "<br>";
            
            foreach (var openWaterTest in actualOpenWaterTests.OrderBy(x => x.Title))
            {
                bodyToResponsible += "<br><br><hr>";
                bodyToResponsible += $"<br><br><h1>{openWaterTest.Title}</h1>";
                bodyToResponsible += $"<br>{openWaterTest.Content}";
            }
        }

        await _mailService.SendEmail(activity.Responsible.Email, activity.Name, bodyToResponsible);

        var bodyToRegistrator = $"Beste {registrator.FirstName},<br><br><br>Je inschrijving voor {activity.Name} werd doorgegeven aan {activity.Responsible.FirstName}.<br><br><br>Met vriendelijke groeten,<br>The Sharks";
        await _mailService.SendEmail(registrator.Email, activity.Name, bodyToRegistrator);
    }
}