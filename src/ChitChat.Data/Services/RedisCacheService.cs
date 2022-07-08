using StackExchange.Redis;

namespace ChitChat.Data.Services;

public class RedisCacheService : ICacheService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    public List<string> GetAllKeysAsync()
    {
       
        List<string> keyList = new List<string>();
        var keys =  _connectionMultiplexer.GetServer("localhost", 6379).Keys();
            keyList.AddRange(keys.Select(key => (string)key).ToList());
 
        return keyList;
    }

    public async Task<string> GetCachValueAsync(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        return await db.StringGetAsync(key);
    }

    public async Task SetCachValueAsync(string key, string value)
    {
        var db = _connectionMultiplexer.GetDatabase();
        await db.StringSetAsync(key, value);
        await db.KeyExpireAsync(key, DateTime.UtcNow.AddSeconds(60));
    }
}
