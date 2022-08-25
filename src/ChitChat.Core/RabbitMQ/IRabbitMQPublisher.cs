using ChitChat.Infrastructure.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Infrastructure.RabbitMQ;
public interface IRabbitMQPublisher
{
    Task SendMessageToQueue(Message message);
}
