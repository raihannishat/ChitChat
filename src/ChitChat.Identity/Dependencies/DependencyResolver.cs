namespace ChitChat.Identity.Dependencies;

public static class DependencyResolver
{
    public static void IdentityResolver(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenHelper, TokenHelper>();

        services.AddSingleton<IJwtSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<JwtSettings>>().Value);
    }
}