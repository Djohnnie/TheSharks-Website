using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Services.Email;
using TheSharks.Domain.Entities;
using TheSharks.Domain.Extensions;

namespace TheSharks.Application.Handlers.Activities.Queries.UpdateActivity.Event;

public class UpdateEventActivityQueryHandler : IRequestHandler<UpdateEventActivityQuery, BaseIdModel>
{
    private readonly IRepository<EventActivity> _eventActivityRepository;
    private readonly IMemberEnrollmentRepository<MemberEnrollment> _memberEnrollmentRepository;
    private readonly IMailService _mailService;

    public UpdateEventActivityQueryHandler(IRepository<EventActivity> eventActivityRepository, IMemberEnrollmentRepository<MemberEnrollment> memberEnrollmentRepository, IMailService mailService)
    {
        _eventActivityRepository = eventActivityRepository;
        _memberEnrollmentRepository = memberEnrollmentRepository;
        _mailService = mailService;
    }

    public async Task<BaseIdModel> Handle(UpdateEventActivityQuery request, CancellationToken cancellationToken)
    {
        var activity = await _eventActivityRepository.Find(x => x.Id.Equals(request.Id));
        var initialDate = activity.Date;

        await _eventActivityRepository.Detach(activity);
        var result = await _eventActivityRepository.Update(new EventActivity
        {
            Id = request.Id,
            Name = request.Title,
            Location = request.Location,
            LocationLink = request.LocationLink,
            Info = request.Info,
            MemberInfo = request.MemberInfo,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Departure = request.Departure,
            Date = request.Date
        },
            x => x.Location,
            x => x.LocationLink,
            x => x.Info,
            x => x.MemberInfo,
            x => x.StartTime,
            x => x.EndTime,
            x => x.Departure,
            x => x.Date);

        if (initialDate != request.Date)
        {
            var enrollments = await _memberEnrollmentRepository.GetAll(x => x.Activity.Id.Equals(request.Id), x => x.Registrator);
            enrollments.ToList().ForEach(x => _mailService.SendEmail(x.Registrator.Email,
                "Verplaatsing TheSharks activiteit",
                $"<div style=\"font-size: 1.1rem\"><p>Hi {x.Registrator.FirstName}</p>" +
                $"<p><b>Een activiteit waar je voor ingeschreven bent werd recent verplaatst.</b></p><br>" +
                $"<div style=\"text-align: center\"><p style=\"font-size: 1.5rem; color: red\"><b>{activity.Name}</b></p>" +
                $"<p>zal plaatsvinden op</p>" +
                $"<p style=\"font-size: 1.2rem\"><b>{DateTimeExtensions.LongDateFormat(result.Date, "nl-NL")}</b></p> " +
                $"<p>in plaats van</p>" +
                $"<p>{DateTimeExtensions.LongDateFormat(initialDate.Date, "nl-NL")}</p></div></div>"));
        }

        return new BaseIdModel { Id = result.Id };
    }
}