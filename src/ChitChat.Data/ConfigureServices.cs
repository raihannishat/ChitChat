using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Data;
public static class ConfigureServices
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(options => configuration.GetSection(nameof(MongoDbSettings)));

        services.AddSingleton<IMongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>());

        services.AddSingleton<IConnectionMultiplexer>(x =>
            ConnectionMultiplexer.Connect(configuration.GetValue<string>("RedisConnection")));

        services.AddSingleton<ICacheService, RedisCacheService>();

        return services;
    }
}
