using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatacircleAPI.Models
{
    public enum Salutation 
    {
        Mr = 1,
        Ms = 2,
        NA = 3
    }

    public class Address {
        public int Id { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public Salutation Salutation { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string phone { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
