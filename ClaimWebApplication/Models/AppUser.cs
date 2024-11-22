using Microsoft.AspNetCore.Identity;

namespace ClaimWebApplication.Models
{
    public class AppUser : IdentityUser
    {
        // Custom property for the full name of the user
        public string FullName { get; set; }
    }
}
