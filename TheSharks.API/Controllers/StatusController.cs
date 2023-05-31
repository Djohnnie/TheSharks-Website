using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace TheSharks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private readonly IConfiguration _configuration;

    public StatusController(
        IMemoryCache memoryCache,
        IConfiguration configuration)
    {
        _memoryCache = memoryCache;
        _configuration = configuration;
    }

    [HttpGet("version")]
    public ActionResult GetVersion()
    {
        var version = _memoryCache.Get<string>("VERSION");

        if (string.IsNullOrEmpty(version))
        {
            version = _configuration.GetValue<string>("Version");
            _memoryCache.Set("VERSION", version);
        }

        return Ok(version);
    }
}