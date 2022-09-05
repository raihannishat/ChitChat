using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Data.Configurations;
public interface IRedisSettings
{
    string Endpoint { get; set; }
    string User { get; set; }
    string Password { get; set; }

}
