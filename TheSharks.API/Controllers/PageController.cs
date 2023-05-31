using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheSharks.Application.Handlers.Pages.Queries.AddPage;
using TheSharks.Application.Handlers.Pages.Queries.GetAllPages;
using TheSharks.Application.Handlers.Pages.Queries.GetDefaultMembersPage;
using TheSharks.Application.Handlers.Pages.Queries.GetDefaultPage;
using TheSharks.Application.Handlers.Pages.Queries.GetMenuTree;
using TheSharks.Application.Handlers.Pages.Queries.GetPage;
using TheSharks.Application.Handlers.Pages.Queries.RemovePage;
using TheSharks.Application.Handlers.Pages.Queries.UpdateMenuTree;
using TheSharks.Application.Handlers.Pages.Queries.UpdatePage;

namespace TheSharks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PageController : ControllerBase
{
    private readonly IMediator _mediator;

    public PageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = "ManagePageContentPolicy")]
    public async Task<IActionResult> CreatePage([FromBody] AddPageQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpGet("{link}")]
    public async Task<IActionResult> GetPage([FromRoute] string link)
    {
        return Ok(await _mediator.Send(new GetPageQuery { Link = link }));
    }

    [HttpGet("menuTree")]
    public async Task<IActionResult> GetMenuTree()
    {
        return Ok(await _mediator.Send(new GetMenuTreeQuery()));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPages()
    {
        return Ok(await _mediator.Send(new GetAllPagesQuery()));
    }

    [HttpPut("{Id}")]
    [Authorize(Policy = "ManagePageContentPolicy")]
    public async Task<IActionResult> UpdatePage([FromBody] UpdatePageQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPut("menuTree")]
    [Authorize(Policy = "ManagePageContentPolicy")]
    public async Task<IActionResult> UpdateMenuTree([FromBody] UpdateMenuTreeQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpDelete("{Id}")]
    [Authorize(Policy = "ManagePageContentPolicy")]
    public async Task<IActionResult> RemovePage([FromRoute] RemovePageQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpGet("members/default")]
    public async Task<IActionResult> GetDefaultMembersPage()
    {
        return Ok(await _mediator.Send(new GetDefaultMembersPageQuery()));
    }

    [HttpGet("default")]
    public async Task<IActionResult> GetDefaultPage()
    {
        return Ok(await _mediator.Send(new GetDefaultPageQuery()));
    }
}