using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheSharks.Application.Handlers.NewsItems.Queries.AddNewsItem;
using TheSharks.Application.Handlers.NewsItems.Queries.GetAllNewsItems;
using TheSharks.Application.Handlers.NewsItems.Queries.GetNewsItem;
using TheSharks.Application.Handlers.NewsItems.Queries.RemoveNewsItem;
using TheSharks.Application.Handlers.NewsItems.Queries.UpdateNewsItem;

namespace TheSharks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public NewsItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = "ManageNewsItemsPolicy")]
    public async Task<IActionResult> CreateNewsItem([FromBody] AddNewsItemQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPut]
    [Authorize(Policy = "ManageNewsItemsPolicy")]
    public async Task<IActionResult> UpdateNewsItem([FromBody] UpdateNewsItemQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "ManageNewsItemsPolicy")]
    public async Task<IActionResult> RemoveNewsItem([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new RemoveNewsItemQuery { Id = id }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNewsItem([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new GetNewsItemQuery { Id = id }));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNewsItems([FromQuery] GetAllNewsItemsQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }
}