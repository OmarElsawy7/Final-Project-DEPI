using Microsoft.AspNetCore.Identity;

namespace EventHub.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    }
}
