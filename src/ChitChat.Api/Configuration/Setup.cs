namespace ChitChat.Api.Configuration;

using ChitChat.Infrastructure.Dependencies;
using ChitChat.Infrastructure.RabbitMQ;
using ChitChat.Infrastructure.SignalR;

public static class Setup
{
    public static void ConfgiureServices(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection(nameof(MongoDbSettings)));
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtSettings:Secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ClockSkew = TimeSpan.FromSeconds(0),
        };

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = tokenValidationParameters;
        });

        builder.Services.AddSingleton(tokenValidationParameters);
        builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
        ConnectionMultiplexer.Connect(builder.Configuration.GetValue<string>("RedisConnection")));

        builder.Services.AddControllers().AddFluentValidation(c =>
            c.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddSwaggerGen(options =>
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
                new string[] {}
            }
            });
        });

        builder.Services.AddSignalR(hubOptions =>
        {
            hubOptions.EnableDetailedErrors = true;
            hubOptions.MaximumReceiveMessageSize = 102400000;
        });

        builder.Services.CoreResolver();
        builder.Services.IdentityResolver();
        builder.Services.DataResolver();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors();
    }

    public static void Configure(this WebApplication app, IApplicationBuilder appBuilder)
    {

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseCors(options => options
        //    .WithOrigins("http://localhost:4200")
        //    .AllowAnyHeader()
        //    .AllowAnyMethod());

        app.UseCors(x => x
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("http://localhost:4200"));

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MapHub<MessageHub>("hubs/message");

        app.Lifetime.ApplicationStarted.Register(() => RegisterSignalRWithRabbitMQ(appBuilder.ApplicationServices));
    }

    public static void ConfigureLogger(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, lc) => lc
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(builder.Configuration));
    }

    public static void RegisterSignalRWithRabbitMQ(IServiceProvider serviceProvider)
    {
        var signalRConsumer = (ISignalRConsumer)serviceProvider.GetService(typeof(ISignalRConsumer));
        signalRConsumer.Connect();

        var dbConsumer = (IDBConsumer)serviceProvider.GetService(typeof(IDBConsumer));
        dbConsumer.Connect();
    }
}
