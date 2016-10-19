using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DatacircleAPI.Models
{
    public class Newsletter
    {
        [Key, Column("Id")]
        public int ID { get; set; }
        [Column(TypeName = "varchar(100)")]                
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
