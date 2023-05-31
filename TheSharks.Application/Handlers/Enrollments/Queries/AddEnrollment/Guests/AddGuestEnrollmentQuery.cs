using MediatR;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Models.Enrollments;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Enrollments.Queries.AddEnrollment.Guests;

public class AddGuestEnrollmentQuery : IRequest<IEnumerable<BaseIdModel>>
{
    public Activity Activity { get; set; }
    public Member Registrator { get; set; }
    public IEnumerable<EnrollmentGuestModel> GuestEnrollments { get; set; }
}