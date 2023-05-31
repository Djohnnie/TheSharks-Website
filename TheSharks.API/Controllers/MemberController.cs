using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheSharks.Application.Handlers.Identity.Queries.Roles.GetRoleStateOfMember;
using TheSharks.Application.Handlers.Identity.Queries.Roles.UpdateRolesOfMember;
using TheSharks.Application.Handlers.Members.EditMember;
using TheSharks.Application.Handlers.Members.EditProfile;
using TheSharks.Application.Handlers.Members.GetAllMembers;
using TheSharks.Application.Handlers.Members.GetAllMembersAndDiveRoles;
using TheSharks.Application.Handlers.Members.GetAllMembersWithRoles;
using TheSharks.Application.Handlers.Members.GetMember;
using TheSharks.Application.Handlers.Members.GetProfilePicture;
using TheSharks.Application.Handlers.Members.RemoveMember;
using TheSharks.Application.Handlers.Members.RemoveMyself;
using TheSharks.Application.Handlers.Members.ResetPassword;
using TheSharks.Application.Handlers.Members.SendMail;
using TheSharks.Application.Handlers.NewsItems.Queries.GetNewsItemsOfMember;
using TheSharks.Contracts.Models.Members;
using TheSharks.Contracts.Models.Pagination;
using TheSharks.Domain.Entities;

namespace TheSharks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MemberController : ControllerBase
{
    private readonly IMediator _mediator;

    public MemberController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetMemberModel>> GetMember([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new GetMemberQuery { Id = id }));
    }

    [HttpGet("{id}/profile-picture")]
    public async Task<ActionResult<GetMemberModel>> GetProfilePicture([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new GetProfilePictureQuery { Id = id }));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<MemberOverviewModel>> GetMemberOverview()
    {
        return Ok(await _mediator.Send(new GetAllMembersQuery()));
    }

    [HttpGet("AndDiveLabels")]
    [Authorize]
    public async Task<ActionResult<MemberOverviewModel>> GetMembersAndDiveLabels([FromQuery] Guid RegistratorId)
    {
        return Ok(await _mediator.Send(new GetAllMembersAndDiveRolesQuery { Id = RegistratorId }));
    }

    [HttpGet("AndRoles")]
    //[Authorize]
    public async Task<ActionResult<MemberRolesOverviewListModel>> GetMemberAndRoles()
    {
        return Ok(await _mediator.Send(new GetAllMembersWithRolesQuery()));
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Member>> PutProfile([FromForm] EditProfileQuery queryRequest, [FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPut("{id}/edit")]
    [Authorize]
    public async Task<ActionResult<Member>> PutMember([FromBody] EditMemberQuery queryRequest, [FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPut("{id}/reset-password")]
    public async Task<ActionResult<Member>> ResetPassword([FromBody] ResetPasswordQuery queryRequest, [FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpGet("{id}/newsitems")]
    public async Task<ActionResult<Member>> GetNewsItemsOfMember([FromRoute] Guid id, [FromQuery] PaginationBaseModel pageMetaData)
    {
        return Ok(await _mediator.Send(new GetNewsItemsOfMemberQuery { Id = id, Page = pageMetaData.Page, RecordsPerPage = pageMetaData.RecordsPerPage }));
    }

    [HttpGet("{id}/roles")]
    [Authorize(Policy = "ManageMembersPolicy")]
    public async Task<IActionResult> GetRoleStateOfMember([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new GetRoleStateOfMemberQuery { Id = id }));
    }

    [HttpPut("{id}/roles")]
    [Authorize(Policy = "ManageMembersPolicy")]
    public async Task<IActionResult> UpdateMemberRoles([FromRoute] Guid id, [FromBody] UpdateRolesOfMemberQuery queryRequest)
    {
        queryRequest.MemberId = id;
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPost("send-mail")]
    [Authorize]
    public async Task<IActionResult> SendMailToMembers([FromBody] SendMailQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "ManageMembersPolicy")]
    public async Task<IActionResult> Deactivate([FromRoute] Guid id)
    {
        var commandQuery = new RemoveMemberCommand { Id = id };
        return Ok(await _mediator.Send(commandQuery));
    }

    [HttpDelete("me/{id}")]
    [Authorize]
    public async Task<IActionResult> DeactivateMyself([FromRoute] Guid id)
    {
        var commandQuery = new RemoveMyselfCommand { Id = id };
        return Ok(await _mediator.Send(commandQuery));
    }
}