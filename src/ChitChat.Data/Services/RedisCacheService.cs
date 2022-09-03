namespace ChitChat.Data.Services;

public class RedisCacheService : ICacheService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly string _publicEndpoint = "redis-15117.c13.us-east-1-3.ec2.cloud.redislabs.com:15117";
    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    public List<string> GetAllKeys()
    {
        var pattern = "DIU_Raizor";

        var keys = _connectionMultiplexer
            .GetServer(_publicEndpoint)
            .Keys(0, pattern + "*")
            .ToList();

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
