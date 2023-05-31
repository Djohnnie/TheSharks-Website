using AutoMapper;
using MediatR;
using TheSharks.Contracts.Models.Identity.Roles;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Identity.Queries.Roles.GetDiveCertificates;

public class GetDiveCertificatesQueryHandler : IRequestHandler<GetDiveCertificatesQuery, GetDiveCertificatesModel>
{
    private readonly IRoleService<Role> _roleService;
    private readonly IMapper _mapper;

    public GetDiveCertificatesQueryHandler(
        IRoleService<Role> roleService,
        IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    public async Task<GetDiveCertificatesModel> Handle(GetDiveCertificatesQuery request, CancellationToken cancellationToken)
    {
        var result = await _roleService.GetAllRoles();

        return new GetDiveCertificatesModel
        {
            DiveCertificates = _mapper.Map<List<DiveCertificateModel>>(result.Where(x => x.ConcernsDivingCertificate))
        };
    }
}