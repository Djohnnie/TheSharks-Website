using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheSharks.Application.Handlers.Activities.Queries.AddActivity.BoardMeeting;
using TheSharks.Application.Handlers.Activities.Queries.AddActivity.Dive;
using TheSharks.Application.Handlers.Activities.Queries.AddActivity.Event;
using TheSharks.Application.Handlers.Activities.Queries.AddActivity.MonitorBoard;
using TheSharks.Application.Handlers.Activities.Queries.GetActivity.BoardMeeting;
using TheSharks.Application.Handlers.Activities.Queries.GetActivity.Dive;
using TheSharks.Application.Handlers.Activities.Queries.GetActivity.Event;
using TheSharks.Application.Handlers.Activities.Queries.GetActivity.MonitorBoard;
using TheSharks.Application.Handlers.Activities.Queries.GetAllActivities;
using TheSharks.Application.Handlers.Activities.Queries.GetUpcomingEnrolledActivity;
using TheSharks.Application.Handlers.Activities.Queries.RemoveActivity;
using TheSharks.Application.Handlers.Activities.Queries.UpdateActivity.BoardMeeting;
using TheSharks.Application.Handlers.Activities.Queries.UpdateActivity.Dive;
using TheSharks.Application.Handlers.Activities.Queries.UpdateActivity.Event;
using TheSharks.Application.Handlers.Activities.Queries.UpdateActivity.MonitorBoard;
using TheSharks.Contracts.Models.Activities;

namespace TheSharks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivityController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActivityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("upcoming")]
    public async Task<ActionResult> GetUpcomingActivity([FromQuery] GetUpcomingEnrolledActivityQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityOverviewModel>>> GetAllActivities([FromQuery] GetAllActivitiesQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPost("event")]
    [Authorize(Policy = "ManageActivitiesPolicy")]
    public async Task<ActionResult<EventActivityModel>> CreateEventActivity([FromBody] AddEventActivityQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPost("dive")]
    [Authorize(Policy = "ManageActivitiesPolicy")]
    public async Task<ActionResult<DiveActivityModel>> CreateDiveActivity([FromBody] AddDiveActivityQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPost("boardmeeting")]
    [Authorize(Policy = "ManageActivitiesPolicy")]
    public async Task<ActionResult<BoardMeetingActivityModel>> CreateBoardMeetingActivity([FromBody] AddBoardMeetingActivityQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPost("monitorboard")]
    [Authorize(Policy = "ManageActivitiesPolicy")]
    public async Task<ActionResult<MonitorBoardActivityModel>> CreateMonitorBoardActivity([FromBody] AddMonitorBoardActivityQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpGet("event/{id}")]
    public async Task<ActionResult<EventActivityModel>> GetEventActivity([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new GetEventActivityQuery { Id = id }));
    }

    [HttpGet("dive/{id}")]
    public async Task<ActionResult<DiveActivityModel>> GetDiveActivity([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new GetDiveActivityQuery { Id = id }));
    }

    [HttpGet("boardmeeting/{id}")]
    public async Task<ActionResult<BoardMeetingActivityModel>> GetBoardMeetingActivity([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new GetBoardMeetingActivityQuery { Id = id }));
    }

    [HttpGet("monitorboard/{id}")]
    public async Task<ActionResult<MonitorBoardActivityModel>> GetMonitorBoardActivity([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new GetMonitorBoardActivityQuery { Id = id }));
    }

    [HttpDelete("{Id}")]
    [Authorize(Policy = "ManageActivitiesPolicy")]
    public async Task<ActionResult> RemoveActivity([FromRoute] RemoveActivityQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPut("event/{id}")]
    [Authorize(Policy = "ManageActivitiesPolicy")]
    public async Task<ActionResult<EventActivityModel>> UpdateEventActivity([FromBody] UpdateEventActivityQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPut("dive/{id}")]
    [Authorize(Policy = "ManageActivitiesPolicy")]
    public async Task<ActionResult<DiveActivityModel>> UpdateDiveActivity([FromBody] UpdateDiveActivityQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPut("boardmeeting/{id}")]
    [Authorize(Policy = "ManageActivitiesPolicy")]
    public async Task<ActionResult<BoardMeetingActivityModel>> UpdateBoardMeetingActivity([FromBody] UpdateBoardMeetingActivityQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPut("monitorboard/{id}")]
    [Authorize(Policy = "ManageActivitiesPolicy")]
    public async Task<ActionResult<MonitorBoardActivityModel>> UpdateMonitorBoardActivity([FromBody] UpdateMonitorBoardActivityQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }
}