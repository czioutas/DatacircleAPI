using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DatacircleAPI.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DatacircleAPI.Database {
    public class ApplicationDbContext : IdentityDbContext<User, Role, int> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<ConnectionDetails> ConnectionDetails { get; set; }
        public DbSet<Newsletter> Newsletter { get; set; }
        public DbSet<Datasource> Datasource { get; set; }
        public DbSet<Metric> Metric { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.Entity<Role>(entity => entity.ToTable("Roles"));
            builder.Entity<User>(entity => entity.ToTable("Users"));

            builder.Entity<User>().Ignore(c => c.UserName);

            builder.Entity<IdentityUserClaim<int>>(entity => entity.ToTable("UserClaims"));
            builder.Entity<IdentityUserLogin<int>>(entity => entity.ToTable("UserLogins"));
            builder.Entity<IdentityUserToken<int>>(entity => entity.ToTable("UserTokens"));            
            builder.Entity<IdentityRoleClaim<int>>(entity => entity.ToTable("RoleClaims"));
            
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
        }
    }
}