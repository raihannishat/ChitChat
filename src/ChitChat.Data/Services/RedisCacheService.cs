namespace ChitChat.Data.Services;

public class RedisCacheService : ICacheService
{
    public readonly ConnectionMultiplexer connectionMultiplexer;
    private readonly IRedisSettings _redisSettings;
    private const string _pattern = "DIU_Raizor";
    public RedisCacheService(IRedisSettings redisSettings)
    {
        _redisSettings = redisSettings;
        connectionMultiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            EndPoints = { _redisSettings.Endpoint },
            User = _redisSettings.User,
            Password = _redisSettings.Password
        });
    }

    public List<string> GetAllKeys()
    {

        var keys = connectionMultiplexer
            .GetServer(_redisSettings.Endpoint)
            .Keys(0, pattern: $"{_pattern}*")
            .ToList();

        var strings = string.Join("", keys);

        var list = strings.Split($"{_pattern}:").ToList();

        list.RemoveAt(0);

        return list;
    }

    public async Task<string> GetCachValueAsync(string key)
    {
        var db = connectionMultiplexer.GetDatabase();
        return await db.StringGetAsync(key);
    }

    public async Task SetCachValueAsync(string key, string value)
    {
        var db = connectionMultiplexer.GetDatabase();
        var prefix = $"{_pattern}:";
        await db.StringSetAsync(prefix + key, value);
        await db.KeyExpireAsync(prefix + key, DateTime.UtcNow.AddSeconds(60));
    }
}
