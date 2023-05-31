using MediatR;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Models.Enrollments;

namespace TheSharks.Application.Handlers.Enrollments.Queries.AddEnrollment.EnrollmentDelegator;

public class EnrollmentDelegatorQuery : IRequest<IEnumerable<BaseIdModel>>
{
    public Guid ActivityId { get; set; }
    public Guid RegistratorId { get; set; }
    public IEnumerable<EnrollmentMemberModel> MemberEnrollments { get; set; }
    public IEnumerable<EnrollmentGuestModel> GuestEnrollments { get; set; } = Enumerable.Empty<EnrollmentGuestModel>();
}