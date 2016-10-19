using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DatacircleAPI.Models
{
    public enum Salutation 
    {
        Mr = 1,
        Ms = 2,
        NA = 3
    }

    public class Address {
        [Key, Column("Id")]
        public int ID { get; set; }
        [Column(TypeName = "varchar(100)")]                
        public string Street { get; set; }
        [Column(TypeName = "varchar(50)")]                
        public string PostCode { get; set; }
        public Salutation Salutation { get; set; }
        [Column(TypeName = "varchar(100)")]                
        public string Country { get; set; }
        [Column(TypeName = "varchar(100)")]                
        public string City { get; set; }
        [Column(TypeName = "varchar(100)")]                
        public string phone { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
