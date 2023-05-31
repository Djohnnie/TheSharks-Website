using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheSharks.Application.Handlers.OpenWaterTests.Commands.AddOpenWaterTest;
using TheSharks.Application.Handlers.OpenWaterTests.Commands.RemoveOpenWaterTest;
using TheSharks.Application.Handlers.OpenWaterTests.Commands.UpdateOpenWaterTest;
using TheSharks.Application.Handlers.OpenWaterTests.Queries.GetAllOpenWaterTests;
using TheSharks.Application.Handlers.OpenWaterTests.Queries.GetOpenWaterTest;

namespace TheSharks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OpenWaterTestController : ControllerBase
{
    private readonly IMediator _mediator;

    public OpenWaterTestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllOpenWaterTests([FromQuery] GetAllOpenWaterTestsQuery request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetOpenWaterTest([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new GetOpenWaterTestQuery { Id = id }));
    }

    [HttpPost]
    [Authorize(Policy = "ManageActivitiesPolicy")]
    public async Task<IActionResult> AddOpenWaterTest([FromBody] AddOpenWaterTestCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "ManageActivitiesPolicy")]
    public async Task<IActionResult> UpdateOpenWaterTest([FromRoute] Guid id, [FromBody] UpdateOpenWaterTestCommand request)
    {
        request.Id = id;
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "ManageActivitiesPolicy")]
    public async Task<IActionResult> RemoveOpenWaterTest([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new RemoveOpenWaterTestCommand { Id = id }));
    }
}