using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Infrastructure.Configuration;
public class RabbitMQSettings : IRabbitMQSettings
{
    public string URI { get; set; } = string.Empty;
}
