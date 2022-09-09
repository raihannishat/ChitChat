namespace ChitChat.Infrastructure.Configuration;

public class RabbitMQSettings : IRabbitMQSettings
{
    public string URI { get; set; } = string.Empty;
}
