namespace ChitChat.Identity.Dependencies;

public static class DependencyResolver
{
    public static void IdentityResolver(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
    }
}