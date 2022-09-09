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
        try
        {
            var value = _cacheService.GetAllKeys();

            return Ok(value);
        }
        catch
        {
            return NotFound();
        }

    }

    [HttpPost("activeNotifying")]
    public async Task<IActionResult> Post([FromBody] CacheEntryDTO request)
    {
        if (string.IsNullOrEmpty(request.Key))
        {
            return BadRequest("Key can not be null");
        }
        try
        {
            await _cacheService.SetCachValueAsync(request.Key, request.Value);

            return Ok();
        }
        catch
        {
            return BadRequest();
        }

    }
}
