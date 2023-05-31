using MediatR;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Enrollments.Queries.RemoveEnrollment;

public class RemoveEnrollmentQuery : IRequest<BaseIdModel>
{
    public Guid ActivityId { get; set; }
    public Guid MemberId { get; set; }
}