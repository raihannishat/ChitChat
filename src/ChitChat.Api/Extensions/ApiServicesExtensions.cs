namespace ChitChat.Api.Extensions;

public static class ApiServicesExtensions
{
    public static void UseAllMiddlewares(this WebApplication app, IApplicationBuilder appBuilder)
    {
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

        app.Lifetime.ApplicationStarted.Register(() => RegisterSignalRWithRabbitMQ(appBuilder.ApplicationServices));
    }

    public static void RegisterSignalRWithRabbitMQ(IServiceProvider serviceProvider)
    {
        var signalRConsumer = (ISignalRConsumer?)serviceProvider.GetService(typeof(ISignalRConsumer));
        signalRConsumer!.Connect();

        var dbConsumer = (IDBConsumer?)serviceProvider.GetService(typeof(IDBConsumer));
        dbConsumer!.Connect();
    }

}
