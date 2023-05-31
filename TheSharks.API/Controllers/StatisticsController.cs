using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheSharks.Application.Handlers.Statistics.Queries.GetAllStatistics;

namespace TheSharks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatisticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatisticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = "ManageStatisticsPolicy")]
    public async Task<ActionResult> GetStatistics([FromQuery] GetAllStatisticsQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }
}