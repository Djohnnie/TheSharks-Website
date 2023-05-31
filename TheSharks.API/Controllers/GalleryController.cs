using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheSharks.Application.Handlers.Galleries.Queries.AddGallery;
using TheSharks.Application.Handlers.Galleries.Queries.AddPictureToGallery;
using TheSharks.Application.Handlers.Galleries.Queries.GetAllGalleries;
using TheSharks.Application.Handlers.Galleries.Queries.GetGallery;
using TheSharks.Application.Handlers.Galleries.Queries.RemoveGallery;
using TheSharks.Application.Handlers.Galleries.Queries.RemovePicturesFromGallery;

namespace TheSharks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GalleryController : ControllerBase
{
    private readonly IMediator _mediator;

    public GalleryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{Id}")]
    [Authorize]
    public async Task<IActionResult> GetGallery([FromRoute] GetGalleryQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPost]
    [Authorize(Policy = "ManageGalleriesPolicy")]
    public async Task<IActionResult> CreateGallery([FromBody] AddGalleryQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetGalleries([FromQuery] GetAllGalleriesQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpDelete("{Id}")]
    [Authorize(Policy = "ManageGalleriesPolicy")]
    public async Task<IActionResult> RemoveGallery([FromRoute] RemoveGalleryQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpPost("{Id}/pictures")]
    [Authorize(Policy = "ManageGalleriesPolicy")]
    public async Task<IActionResult> AddPicturesToGallery([FromForm] AddPictureToGalleryQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }

    [HttpDelete("{Id}/pictures")]
    [Authorize(Policy = "ManageGalleriesPolicy")]
    public async Task<IActionResult> RemovePicturesFromGallery([FromBody] RemovePicturesFromGalleryQuery queryRequest)
    {
        return Ok(await _mediator.Send(queryRequest));
    }
}