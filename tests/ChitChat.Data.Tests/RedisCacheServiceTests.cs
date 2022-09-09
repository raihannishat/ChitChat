namespace ChitChat.Data.Tests;

public class RedisCacheServiceTests
{
    private readonly RedisCacheService _sut;
    private readonly Mock<IRedisSettings> _redisSettingsMock = new();
    private readonly Mock<ConnectionMultiplexer> _connctionMultiplexerMock = new();

    public RedisCacheServiceTests()
    {
        _sut = new RedisCacheService(_redisSettingsMock.Object);
    }
}
