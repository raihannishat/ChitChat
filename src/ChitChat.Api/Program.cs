var builder = WebApplication.CreateBuilder(args);

builder.ConfigureLogger();
builder.ConfgiureServices();

var app = builder.Build();
app.Configure(app);

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