using ChitChat.Data;
using ChitChat.Data.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Core.Documents
{
    [BsonCollection("Users")]
    public class User : Document
    {
        public string Name { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
