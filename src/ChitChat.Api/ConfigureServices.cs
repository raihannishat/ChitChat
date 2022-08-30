namespace ChitChat.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddControllers().AddFluentValidation(c =>
            c.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        services.AddEndpointsApiExplorer();
        services.AddCors();
        services.AddOptions();
        return services;
    }

    public static void RegisterSignalRWithRabbitMQ(IServiceProvider serviceProvider)
    {
        var signalRConsumer = (ISignalRConsumer?)serviceProvider.GetService(typeof(ISignalRConsumer));
        signalRConsumer!.Connect();

        var dbConsumer = (IDBConsumer?)serviceProvider.GetService(typeof(IDBConsumer));
        dbConsumer!.Connect();
    }
}
