using ChitChat.Data.Services;
using Microsoft.AspNetCore.Mvc;


namespace ChitChat.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
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
    public async Task<IActionResult> GetAllKeys()
    {
        var value = _cacheService.GetAllKeysAsync();
       
        return Ok(value);
    }

    [HttpPost("activeNotifying")]
    public async Task<IActionResult> Post([FromBody] CacheEntryRequest request)
    {
        await _cacheService.SetCachValueAsync(request.Key, request.Value);
        return Ok();
    }

  
}
