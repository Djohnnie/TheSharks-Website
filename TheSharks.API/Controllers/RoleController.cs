using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheSharks.Application.Handlers.Identity.Queries.Roles.AddRole;
using TheSharks.Application.Handlers.Identity.Queries.Roles.GetAllRoles;
using TheSharks.Application.Handlers.Identity.Queries.Roles.GetDiveCertificates;
using TheSharks.Application.Handlers.Identity.Queries.Roles.GetRole;
using TheSharks.Application.Handlers.Identity.Queries.Roles.RemoveRole;
using TheSharks.Application.Handlers.Identity.Queries.Roles.UpdateRole;

namespace TheSharks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = "ManageMembersPolicy")]
    public async Task<IActionResult> CreateRole([FromBody] AddRoleQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "ManageMembersPolicy")]
    public async Task<IActionResult> GetRole([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new GetRoleQuery { Id = id }));
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "ManageMembersPolicy")]
    public async Task<IActionResult> UpdateRole([FromRoute] Guid id, [FromBody] UpdateRoleQuery queryRequest)
    {
        queryRequest.Id = id;
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "ManageMembersPolicy")]
    public async Task<IActionResult> RemoveRole([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new RemoveRoleQuery { Id = id }));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllRoles()
    {
        return Ok(await _mediator.Send(new GetAllRolesQuery()));
    }

    [HttpGet("divecertificates")]
    [Authorize]
    public async Task<IActionResult> GetDiveCertificates()
    {
        return Ok(await _mediator.Send(new GetDiveCertificatesQuery()));
    }
}