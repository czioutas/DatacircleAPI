using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatacircleAPI.Models
{
    public class Company
    {
        [Key, Column("Id")]
        public int ID { get; set; }
        [Column(TypeName = "varchar(100)")]                
        public string Name { get; set; }
        [Column(TypeName = "varchar(100)")]                
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
