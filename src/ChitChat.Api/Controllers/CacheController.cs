
namespace ChitChat.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class CacheController : ControllerBase
{
    private readonly ICacheService _cacheService;

    public CacheController(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    [HttpGet("get/{key}")]
    public async Task<IActionResult> Get([FromRoute] string key)
    {
        var value = await _cacheService.GetCachValueAsync(key);

        if (value == null)
        {
            return NotFound("Key does'not exist");
        }

        return Ok(value);
    }

    [HttpGet("activeUsers")]
    public IActionResult GetAllKeys()
    {
        var value = _cacheService.GetAllKeys();

        return Ok(value);
    }

    [HttpPost("activeNotifying")]
    public async Task<IActionResult> Post([FromBody] CacheEntryDTO request)
    {
        await _cacheService.SetCachValueAsync(request.Key, request.Value);

        return Ok();
    }
}
