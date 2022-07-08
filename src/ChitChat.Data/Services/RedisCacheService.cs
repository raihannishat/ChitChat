using StackExchange.Redis;

namespace ChitChat.Data.Services;

public class RedisCacheService : ICacheService
{
    private readonly List<string> _listKeys;
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _listKeys = new List<string>();

    }

    public List<string> GetAllKeysAsync()
    {
        //var keys =  _connectionMultiplexer.GetServer("localhost", 6379).Keys();
        //ListKeys = keys;

        using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379"))
        {
            var keys = redis.GetServer("localhost", 6379).Keys();
            _listKeys.AddRange(keys.Select(key => (string)key).ToList());

        }
        return _listKeys;
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
