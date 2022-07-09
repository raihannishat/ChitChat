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
        var pattern = "DIU_Raizor";

        var keys = _connectionMultiplexer
            .GetServer("localhost", 6379).Keys(0, pattern + "*").ToList();
        
        var strings = string.Join("", keys);
        
        var list = strings.Split("DIU_Raizor:").ToList();
        list.RemoveAt(0);

        return list;
    }

    public async Task<string> GetCachValueAsync(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        return await db.StringGetAsync(key);
    }

    public async Task SetCachValueAsync(string key, string value)
    {
        var db = _connectionMultiplexer.GetDatabase();
        var prefix = "DIU_Raizor:";
        await db.StringSetAsync(prefix + key, value);
        await db.KeyExpireAsync(prefix + key, DateTime.UtcNow.AddSeconds(60));
    }
}
