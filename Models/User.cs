using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace DatacircleAPI.Models
{
    public class User : IdentityUser<int> {
        [Column(TypeName = "varchar(100)")]                
        public string FirstName { get; set; }
        [Column(TypeName = "varchar(100)")]                
        public string MiddleName { get; set; }
        [Column(TypeName = "varchar(100)")]                
        public string LastName { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public bool IsCompanyOwner { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "varchar(250)")]                
        public string VerificationKey { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string Token { get; set; }
        public bool IsVerified { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
