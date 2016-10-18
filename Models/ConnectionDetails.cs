using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatacircleAPI.Models
{
    public class ConnectionDetails
    {
        public int ID { get; set; }
        public string ConnectionString { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public int Port { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
