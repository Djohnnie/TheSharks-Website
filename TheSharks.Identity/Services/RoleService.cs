using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using System.Security.Claims;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Services.Identity;
using TheSharks.Domain.Entities;

namespace TheSharks.Identity.Services;

public class RoleService : IRoleService<Role>
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<Member> _userManager;

    public RoleService(RoleManager<Role> roleManager, UserManager<Member> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<Role> CreateRole(string roleName, bool concernsDivingCertificate, Dictionary<string, string> toAssignClaims)
    {
        if (await _roleManager.RoleExistsAsync(roleName)) throw new RoleException("Deze rol bestaat al");

        var createdRole = await _roleManager.CreateAsync(new Role { Name = roleName, ConcernsDivingCertificate = concernsDivingCertificate });
        if (!createdRole.Succeeded) throw new RoleException("Probleem bij het toevoegen van de rol");

        var role = await _roleManager.FindByNameAsync(roleName);
        foreach (var privilege in toAssignClaims)
        {
            await _roleManager.AddClaimAsync(role, new Claim(privilege.Key, privilege.Value));
        }

        return role;
    }

    public async Task<IList<Role>> GetAllRoles()
    {
        return _roleManager.Roles.OrderBy(x => x.Name).ToList();
    }

    public async Task<IList<Role>> GetAllRoles(Expression<Func<Role, bool>> filter)
    {
        return _roleManager.Roles.Where(filter).OrderBy(x => x.Name).ToList();
    }

    public async Task<IEnumerable<Claim>> GetClaimsOfRole(Role role)
    {
        return await _roleManager.GetClaimsAsync(role);
    }

    public async Task<IList<Role>> GetRolesAssignedToMember(Guid memberId)
    {
        var member = await _userManager.FindByIdAsync(memberId.ToString());
        if (member == null) throw new IdentityException("Systeem error");

        var roleNames = await _userManager.GetRolesAsync(member);
        return _roleManager.Roles.Where(x => roleNames.Contains(x.Name)).ToList();
    }

    public async Task<Role> GetRole(Guid id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        if (role == null) throw new RoleException("Deze rol bestaat niet");
        return role;
    }

    public async Task<Guid> UpdateRolesOfMember(Guid memberId, IEnumerable<string> roleNames)
    {
        var member = await _userManager.FindByIdAsync(memberId.ToString());
        if (member == null) throw new IdentityException("Systeem error");

        var currentlyAssignedRoles = await _userManager.GetRolesAsync(member);
        await _userManager.RemoveFromRolesAsync(member, currentlyAssignedRoles);

        await _userManager.AddToRolesAsync(member, roleNames);
        return member.Id;
    }

    public async Task<Guid> UpdateClaimsOfRole(Guid id, Dictionary<string, string> toAssignClaims)
    {
        var role = await GetRole(id);
        var claims = await GetClaimsOfRole(role);

        foreach (var claim in claims)
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }

        foreach (var privilege in toAssignClaims)
        {
            await _roleManager.AddClaimAsync(role, new Claim(privilege.Key, privilege.Value));
        }

        return role.Id;
    }

    public async Task<IList<Role>> GetRolesAssignedToMember(Guid memberId, Expression<Func<Role, bool>> filter)
    {
        var member = await _userManager.FindByIdAsync(memberId.ToString());
        if (member == null) throw new IdentityException("Systeem error");

        var roleNames = await _userManager.GetRolesAsync(member);
        return _roleManager.Roles.Where(filter).Where(x => roleNames.Contains(x.Name)).ToList();
    }

    public async Task RemoveRole(Role role)
    {
        var deletedRole = await _roleManager.DeleteAsync(role);
        if (!deletedRole.Succeeded) throw new RoleException("Er ging iets fout bij het verwijderen");
    }
}