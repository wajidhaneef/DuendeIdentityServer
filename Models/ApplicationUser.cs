using Microsoft.AspNetCore.Identity;

namespace DuendeIdentityServer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? UserEmail { get; set; }
        public string? Password { get; set; }

    }
}