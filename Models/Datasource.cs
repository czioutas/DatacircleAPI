using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Key, Column("Id")]
        public int ID { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]                
        public string Title { get; set; }
        [Column(TypeName = "varchar(100)")]                
        public string Description { get; set; }
        public Type Type { get; set; }
        public int ConnectionDetailsFk { get; set; }
        [ForeignKey("ConnectionDetailsFk")]
        public ConnectionDetails ConnectionDetails { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public void addMore()
        {
            
        }
    }


}
