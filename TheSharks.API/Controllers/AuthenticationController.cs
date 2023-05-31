using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheSharks.Application.Handlers.Identity.Queries.Authentication.ForgotPassword;
using TheSharks.Application.Handlers.Identity.Queries.Authentication.Login;
using TheSharks.Application.Handlers.Identity.Queries.Authentication.Register;
using TheSharks.Contracts.Models.Identity.Authentication;
using TheSharks.Domain.Entities;

namespace TheSharks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPost("register")]
    [Authorize(Policy = "ManageMembersPolicy")]
    public async Task<ActionResult<Member>> Register([FromBody] RegisterQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPost("forgot-password")]
    public async Task<ActionResult<Member>> ForgotPassword([FromBody] ForgotPasswordQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }
}