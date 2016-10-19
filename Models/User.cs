using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DatacircleAPI.Models
{
    public class User : IdentityUser<int> {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public Company Company { get; set; }
        public Role Role { get; set; }
        public bool IsCompanyOwner { get; set; }
        public bool IsActive { get; set; }
        public string VerificationKey { get; set; }
        public string Token { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
