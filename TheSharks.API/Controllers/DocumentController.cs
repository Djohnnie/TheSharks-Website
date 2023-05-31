using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheSharks.Application.Handlers.Documents.Queries.AddDocument;
using TheSharks.Application.Handlers.Documents.Queries.DownloadDocument;
using TheSharks.Application.Handlers.Documents.Queries.GetAllDocuments;
using TheSharks.Application.Handlers.Documents.Queries.RemoveDocument;

namespace TheSharks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetDocuments([FromQuery] GetAllDocumentsQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPost]
    [Authorize(Policy = "ManageDownloadablesPolicy")]
    public async Task<IActionResult> CreateDocument([FromForm] AddDocumentQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> DownloadDocument([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new DownloadDocumentQuery { Id = id }));
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "ManageDownloadablesPolicy")]
    public async Task<IActionResult> RemoveDocument([FromQuery] RemoveDocumentQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }
}