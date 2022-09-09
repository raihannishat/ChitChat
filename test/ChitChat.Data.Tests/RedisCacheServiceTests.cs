using ChitChat.Data.Configurations;
using ChitChat.Data.Services;
using Moq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Data.Tests;
public  class RedisCacheServiceTests
{
    private readonly RedisCacheService _sut;
    private readonly Mock<IRedisSettings> _redisSettingsMock = new Mock<IRedisSettings>();
    private readonly Mock<ConnectionMultiplexer> _connctionMultiplexerMock = new Mock<ConnectionMultiplexer>();

    public RedisCacheServiceTests()
    {
        _sut = new RedisCacheService(_redisSettingsMock.Object);
    }

}
