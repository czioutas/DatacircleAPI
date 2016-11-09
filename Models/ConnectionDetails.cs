using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatacircleAPI.Models
{
    public class ConnectionDetails
    {
        [Key, Column("Id")]
        public int ID { get; set; }
        [Column(TypeName = "varchar(250)")]                
        public string ConnectionString { get; set; }
        [Column(TypeName = "varchar(250)")]                
        public string Host { get; set; }
        [Column(TypeName = "varchar(250)")]                
        public string Username { get; set; }
        [Column(TypeName = "text")]                
        public string Password { get; set; }
        [Column(TypeName = "varchar(250)")]                
        public string Database { get; set; }
        public int Port { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
