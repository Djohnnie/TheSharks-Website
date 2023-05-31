using AutoMapper;
using MediatR;
using TheSharks.Contracts.DataAccess;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Models.Common;
using TheSharks.Contracts.Models.Enrollments;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Enrollments.Queries.AddEnrollment.Members;

public class AddMemberEnrollmentQueryHandler : IRequestHandler<AddMemberEnrollmentQuery, IEnumerable<BaseIdModel>>
{
    private readonly IMemberEnrollmentRepository<MemberEnrollment> _enrollmentRepository;
    private readonly IMemberService<Member> _memberService;
    private readonly IMapper _mapper;

    public AddMemberEnrollmentQueryHandler(IMemberEnrollmentRepository<MemberEnrollment> enrollmentRepository, IMemberService<Member> memberService, IMapper mapper)
    {
        _enrollmentRepository = enrollmentRepository;
        _memberService = memberService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BaseIdModel>> Handle(AddMemberEnrollmentQuery request, CancellationToken cancellationToken)
    {
        var enrollments = new List<MemberEnrollment>();

        foreach (EnrollmentMemberModel member in request.MemberEnrollments)
        {
            var registree = await _memberService.Find(member.RegistreeId);
            if (registree == null) return Enumerable.Empty<BaseIdModel>();
            var existingEnrollments = await _enrollmentRepository.GetAllOfActivityAndRegistree(request.Activity.Id, registree.Id);
            if (existingEnrollments.Any()) throw new DuplicateEnrollmentException("Je kan jezelf niet twee keer inschrijven");

            enrollments.Add(new MemberEnrollment
            {
                Activity = request.Activity,
                Registrator = request.Registrator,
                AsDiver = member.AsDiver,
                Remark = member.Remark,
                Registree = registree,
                OpenWaterTestId = member.OpenWaterTestId
            });
        }

        var result = await _enrollmentRepository.Add(enrollments);
        return _mapper.Map<IEnumerable<BaseIdModel>>(result);
    }
}