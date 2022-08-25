using ChitChat.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Core.RabbitMQ;
public interface IRabbitMQPublisher
{
    Task SendMessageToQueue(Message message);
}
