namespace ChitChat.Infrastructure.RabbitMQ;

public interface IRabbitMQPublisher
{
    Task SendMessageToQueue(Message message);
}
