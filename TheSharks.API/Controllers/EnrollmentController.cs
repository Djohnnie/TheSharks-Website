using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheSharks.Application.Handlers.Enrollments.Queries.AddEnrollment.EnrollmentDelegator;
using TheSharks.Application.Handlers.Enrollments.Queries.RemoveEnrollment;

namespace TheSharks.Contracts.controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public EnrollmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateEnrollment([FromBody] EnrollmentDelegatorQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> RemoveEnrollment([FromBody] RemoveEnrollmentQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }
}