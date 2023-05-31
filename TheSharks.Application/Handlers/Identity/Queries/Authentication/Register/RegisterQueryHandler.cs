using MediatR;
using TheSharks.Contracts.Models.Identity.Authentication;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Domain.Entities;

namespace TheSharks.Application.Handlers.Identity.Queries.Authentication.Register;

public class RegisterQueryHandler : IRequestHandler<RegisterQuery, Member>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRoleService<Role> _roleService;

    public RegisterQueryHandler(IAuthenticationService authenticationService, IRoleService<Role> roleService)
    {
        _authenticationService = authenticationService;
        _roleService = roleService;
    }

    public async Task<Member> Handle(RegisterQuery request, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.Register(new RegisterModel
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email
        });

        await _roleService.UpdateRolesOfMember(result.Id, new List<string>() { "Geen" });

        // Fix circular reference.
        result.MemberRoles = null;

        return result;
    }
}