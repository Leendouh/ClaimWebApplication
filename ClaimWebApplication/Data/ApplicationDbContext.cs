using ClaimWebApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClaimWebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        // contructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // empty
        }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

    }
}
