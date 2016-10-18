using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatacircleAPI.Models
{
    public enum Type
    {
        Mysql = 1,
        RabbitMq = 2,
        Push = 3
    }

    public class Datasource
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Type Type { get; set; }
        public IEnumerable<ConnectionDetails> ConnectionDetails { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
