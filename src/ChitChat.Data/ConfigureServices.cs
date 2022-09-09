namespace ChitChat.Data;

public static class ConfigureServices
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>());

        services.AddSingleton<IRedisSettings>(configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>());

        services.AddSingleton<ICacheService, RedisCacheService>();

        return services;
    }
}
