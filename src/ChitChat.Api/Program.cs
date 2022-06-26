using ChitChat.Api.Configuration;
using ChitChat.Identity.Configuration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add serilog configuration for logging
builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration));



builder.Services.ConfgiureServices(builder);

builder.Services.IdentityResolver();
builder.Services.DataResolver();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

builder.Services.Configure(app);
// Configure the HTTP request pipeline.

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