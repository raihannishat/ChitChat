namespace ChitChat.Data.Dependencies;

public static class DependencyResolver
{
    public static void DataResolver(this IServiceCollection services)
    {
        services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
    }
}
