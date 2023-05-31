using DuendeIdentityServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DuendeIdentityServer.Data
{
    public class ApplicationDBContext : IdentityDbContext <ApplicationUser>
    {
        public ApplicationDBContext()
        {
            
        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
