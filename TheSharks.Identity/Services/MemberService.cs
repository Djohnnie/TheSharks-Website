using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TheSharks.Contracts;
using TheSharks.Contracts.Exceptions;
using TheSharks.Contracts.Helpers;
using TheSharks.Contracts.Models.Members;
using TheSharks.Contracts.Services.Email;
using TheSharks.Contracts.Services.Logging;
using TheSharks.Contracts.Services.Members;
using TheSharks.Domain.Entities;
using TheSharks.Domain.Extensions;

namespace TheSharks.Identity.Services;

public class MemberService : IMemberService<Member>
{
    private readonly UserManager<Member> _userManager;
    private readonly IMailService _mailService;
    private readonly ILogService _logService;
    private readonly IPictureHelper _pictureHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MemberService(
        UserManager<Member> userManager,
        IMailService mailService,
        ILogService logService,
        IPictureHelper pictureHelper,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _mailService = mailService;
        _logService = logService;
        _pictureHelper = pictureHelper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Member?> GetCurrent()
    {
        if (_httpContextAccessor.HttpContext?.User == null) throw new IdentityException("Kan gebruiker niet ophalen");
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        return user;
    }

    public async Task<Member> Find(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null) throw new IdentityException("Kan gebruiker niet ophalen");
        return user;
    }

    public async Task<IEnumerable<Member>> GetAll(Expression<Func<Member, bool>> filter)
    {
        return _userManager.Users.Where(x => x.FirstName != "Ex-lid").Where(filter).AsEnumerable();
    }

    public async Task<Member> Update(EditProfileModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id.ToString());
        if (user == null) throw new IdentityException("Kan gebruiker niet ophalen");

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Bio = model.Bio;
        if (model.ProfilePicture != null)
        {
            var profilePicture = await FormFileExtensions.GetBytes(model.ProfilePicture);
            user.ProfilePicture = await _pictureHelper.PreparePicture(profilePicture, 512);
        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) throw new IdentityException("Kan gegevens niet updaten"); ;

        return user;
    }

    public async Task<Member> Update(EditMemberModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id.ToString());
        if (user == null) throw new IdentityException("Kan gebruiker niet ophalen");

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Email = model.Email;
        user.UserName = model.UserName;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) throw new IdentityException("Kan gegevens niet updaten");

        return user;
    }

    public async Task<Member> ResetPassword(ResetPasswordModel model)
    {
        await _logService.RecordLog(LogIdentifier.ResetPassword, $"Password reset for user id '{model.Id}' with token '{model.Token}'.");

        var user = await _userManager.FindByIdAsync(model.Id.ToString());
        if (user == null) throw new IdentityException("Kan gebruiker niet ophalen");

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        if (!result.Succeeded)
        {
            await _logService.RecordLog(LogIdentifier.UnsuccessfulPasswordReset, $"Password reset failed for '{user.UserName}' with token '{model.Token}'.");

            throw new IdentityException("Kan passwoord niet resetten");
        }

        await _mailService.SendEmail(
            user.Email,
            StringConstants.PASSWORD_RESET_CONFIRM_EMAIL_SUBJECT,
            string.Format(StringConstants.PASSWORD_RESET_CONFIRM_EMAIL_BODY, user.FirstName, user.UserName));

        await _logService.RecordLog(LogIdentifier.SuccessfulPasswordReset, $"Pasword reset successful for user '{user.UserName}'.");

        return user;
    }

    public async Task<IEnumerable<Member>> GetAllOrderBy<TProperty>(Expression<Func<Member, TProperty>> orderProperty, bool descending)
    {
        var query = _userManager.Users.Where(x => x.FirstName != "Ex-lid")
            .Include(x => x.MemberRoles).ThenInclude(x => x.Role).AsQueryable();

        if (descending) return query.OrderByDescending(orderProperty);
        else return query.OrderBy(orderProperty);
    }

    public async Task<IEnumerable<Member>> GetAllInRole(string roleName)
    {
        var users = await _userManager.GetUsersInRoleAsync(roleName);
        return users.Where(x => x.FirstName != "Ex-lid");
    }

    public async Task<Member> Deactive(Guid id)
    {
        var user = await Find(id);

        await _userManager.RemovePasswordAsync(user);

        var userRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, userRoles);

        user.LockoutEnabled = true;
        user.LockoutEnd = DateTime.UtcNow.AddYears(1000);
        user.UserName = $"{Guid.NewGuid()}";
        user.Email = null;
        user.FirstName = "Ex-lid";
        user.LastName = string.Empty;
        user.Bio = null;
        user.ProfilePicture = null;

        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded) throw new IdentityException("Lid kan niet worden verwijderd");

        return user;
    }
}