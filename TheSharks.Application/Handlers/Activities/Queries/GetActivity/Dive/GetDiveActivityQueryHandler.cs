using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Helpers;
using TheSharks.Contracts.Models.Activities;
using TheSharks.Contracts.Models.Activities.BaseModels;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Common;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Activities.Queries.GetActivity.Dive;

public class GetDiveActivityQueryHandler : IRequestHandler<GetDiveActivityQuery, DiveActivityModel>
{
    private readonly IRepository<DiveActivity> _activityRepository;
    private readonly IRepository<Enrollment> _enrollmentRepository;
    private readonly IRepository<GuestEnrollment> _guestEnrollmentRepository;
    private readonly IMemberEnrollmentRepository<MemberEnrollment> _memberEnrollmentsRepository;
    private readonly IMemberService<Member> _memberService;
    private readonly IRoleService<Role> _roleService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEncryptionHelper _encryptionHelper;
    private readonly IConfiguration _configuration;

    public GetDiveActivityQueryHandler(
        IRepository<DiveActivity> activityRepository,
        IRepository<Enrollment> enrollmentRepository,
        IMemberEnrollmentRepository<MemberEnrollment> memberEnrollmentRepository,
        IRepository<GuestEnrollment> guestEnrollmentRepository,
        IMemberService<Member> memberService,
        IRoleService<Role> roleService,
        IHttpContextAccessor httpContextAccessor,
        IEncryptionHelper encryptionHelper,
        IConfiguration configuration)
    {
        _activityRepository = activityRepository;
        _enrollmentRepository = enrollmentRepository;
        _memberEnrollmentsRepository = memberEnrollmentRepository;
        _guestEnrollmentRepository = guestEnrollmentRepository;
        _memberService = memberService;
        _roleService = roleService;
        _httpContextAccessor = httpContextAccessor;
        _encryptionHelper = encryptionHelper;
        _configuration = configuration;
    }

    public async Task<DiveActivityModel> Handle(GetDiveActivityQuery request, CancellationToken cancellationToken)
    {
        var authenticatedUserId = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "sid")?.Value;
        var activity = await _activityRepository.Find(x => x.Id.Equals(request.Id), x => x.Responsible);

        var isInfoForResponsible = authenticatedUserId == $"{activity.Responsible.Id}";

        var enrollments = await _enrollmentRepository.GetAll(x => x.Activity.Id.Equals(request.Id));
        var guestEnrollments = await _guestEnrollmentRepository.GetAll(x => x.Activity.Id.Equals(activity.Id), x => x.Registrator, x => x.OpenWaterTest);
        var memberEnrollments = await _memberEnrollmentsRepository.GetAllWithRegistreeAndRegistrator(activity.Id);

        var mappedEnrollments = new List<ActivityEnrollmentModel>();
        var memberIds = memberEnrollments.Select(x => x.Registree.Id);
        var members = await _memberService.GetAll(x => memberIds.Contains(x.Id));

        foreach (var enrollment in enrollments)
        {
            var memberEnrollment = memberEnrollments.SingleOrDefault(x => x.Id == enrollment.Id);
            if (memberEnrollment != null)
            {
                var roles = await _roleService.GetRolesAssignedToMember(memberEnrollment.Registree.Id, x => x.ConcernsDivingCertificate);
                var role = roles.FirstOrDefault();
                var member = await _memberService.Find(memberEnrollment.Registree.Id);
                var extraInfo = GetExtraInfo(member.Bio);

                mappedEnrollments.Add(new ActivityEnrollmentModel
                {
                    RegistratorFirstName = memberEnrollment.Registrator.FirstName,
                    RegistratorLastName = memberEnrollment.Registrator.LastName,
                    DiveCertificate = role != null ? role.Name : null,
                    RegistreeId = memberEnrollment.Registree.Id,
                    Registree = memberEnrollment.Registree.FirstName + " " + memberEnrollment.Registree.LastName,
                    RegistreeEmail = isInfoForResponsible ? member.Email : null,
                    RegistreePhoneNumber = isInfoForResponsible ? extraInfo.PhoneNumber : null,
                    Remark = memberEnrollment.Remark,
                    Id = memberEnrollment.Registree.Id,
                    AsDiver = memberEnrollment.AsDiver,
                    OpenWaterTestTitle = memberEnrollment.OpenWaterTest?.Title,
                    OpenWaterTestContent = memberEnrollment.OpenWaterTest?.Content,
                });
            }

            var guestEnrollment = guestEnrollments.SingleOrDefault(x => x.Id == enrollment.Id);
            if (guestEnrollment != null)
            {
                mappedEnrollments.Add(new ActivityEnrollmentModel
                {
                    RegistratorFirstName = guestEnrollment.Registrator.FirstName,
                    RegistratorLastName = guestEnrollment.Registrator.LastName,
                    DiveCertificate = guestEnrollment.DiveLevel,
                    Registree = guestEnrollment.Registree,
                    Remark = guestEnrollment.Remark,
                    AsDiver = guestEnrollment.AsDiver,
                    OpenWaterTestTitle = guestEnrollment.OpenWaterTest?.Title,
                    OpenWaterTestContent = guestEnrollment.OpenWaterTest?.Content,
                });
            }
        }

        return new DiveActivityModel
        {
            Id = activity.Id,
            Date = activity.Date,
            Name = activity.Name,
            Title = activity.Name,
            ActivityType = activity.ActivityType,
            Info = activity.Info,
            MemberInfo = activity.MemberInfo,
            Location = activity.Location,
            LocationLink = activity.LocationLink,
            AtWater = activity.AtWater,
            BriefingTime = activity.BriefingTime,
            Departure = activity.Departure,
            Tide = activity.Tide,
            NecessarySubscription = activity.NecessarySubscription,
            ResponsibleId = activity.Responsible.Id,
            ResponsibleFirstName = activity.Responsible.FirstName,
            ResponsibleLastName = activity.Responsible.LastName,
            Enrollments = activity.NecessarySubscription ? mappedEnrollments : new List<ActivityEnrollmentModel>()
        };
    }

    private MemberProfileExtraInfo GetExtraInfo(string bio)
    {
        try
        {
            var secureKey = _configuration.GetValue<string>("SecureKey");
            var decryptedBio = _encryptionHelper.DecryptString(bio, secureKey);
            return JsonSerializer.Deserialize<MemberProfileExtraInfo>(decryptedBio);
        }
        catch
        {
            return new MemberProfileExtraInfo();
        }
    }
}