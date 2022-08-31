var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
           .MinimumLevel.Debug()
           .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
           .Enrich.FromLogContext()
           .ReadFrom.Configuration(builder.Configuration));


builder.Services.AddInfractructureServices();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddWebApiServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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