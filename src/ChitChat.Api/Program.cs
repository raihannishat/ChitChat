var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
           .MinimumLevel.Debug()
           .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
           .Enrich.FromLogContext()
           .ReadFrom.Configuration(builder.Configuration));


builder.Services.AddInfractructureServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddWebApiServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorApp =>
    {
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-5.0
        errorApp.Run(async context =>
        {
            // https://datatracker.ietf.org/doc/html/rfc7807#page-6
            var pd = new ProblemDetails
            {
                Type = "https://demo.api.com/errors/internal-server-error",
                Title = "An error occurred whilst processing your request. Contact with admin",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "Oops!!! Something went wrong",
            };
            pd.Extensions.Add("RequestId", context.TraceIdentifier);
            await context.Response.WriteAsJsonAsync(pd, pd.GetType(), null, contentType: "application/problem+json");
        });
    });
}


app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins(builder.Configuration.GetSection("FrontendURL").ToString()!));

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<MessageHub>("hubs/message");
app.Lifetime.ApplicationStarted.Register(() =>
    ChitChat.Api.ConfigureServices.RegisterSignalRWithRabbitMQ(
    ((IApplicationBuilder)(app)).ApplicationServices));

try
{
    Log.Information("Application Starting up");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}