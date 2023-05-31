using AutoMapper;
using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Models.Enrollments;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Enrollments.Queries.AddEnrollment.Guests;

public class AddGuestEnrollmentQueryHandler : IRequestHandler<AddGuestEnrollmentQuery, IEnumerable<BaseIdModel>>
{
    private readonly IRepository<GuestEnrollment> _enrollmentRepository;
    private readonly IMapper _mapper;

    public AddGuestEnrollmentQueryHandler(IRepository<GuestEnrollment> enrollmentRepository, IMapper mapper)
    {
        _enrollmentRepository = enrollmentRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BaseIdModel>> Handle(AddGuestEnrollmentQuery request, CancellationToken cancellationToken)
    {
        var enrollments = new List<GuestEnrollment>();

        foreach (EnrollmentGuestModel guest in request.GuestEnrollments)
        {            
            enrollments.Add(new GuestEnrollment
            {
                Activity = request.Activity,
                AsDiver = guest.AsDiver,
                DiveLevel = guest.DiveCertificate,
                Registrator = request.Registrator,
                Registree = guest.Registree,
                Remark = guest.Remark,
                OpenWaterTestId = guest.OpenWaterTestId
            });
        }

        var result = await _enrollmentRepository.Add(enrollments);
        return _mapper.Map<IEnumerable<BaseIdModel>>(result);
    }
}