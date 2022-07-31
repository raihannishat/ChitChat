namespace ChitChat.Data.Dependencies;

public static class DependencyResolver
{
    public static void DataResolver(this IServiceCollection services)
    {
        services.AddScoped<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

        services.AddSingleton<ICacheService, RedisCacheService>();

    }
}
