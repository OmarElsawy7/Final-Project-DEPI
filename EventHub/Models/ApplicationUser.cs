using Microsoft.AspNetCore.Identity;

namespace EventHub.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Ticket> BoughtTickets { get; set; } = new List<Ticket>();
        public ICollection<Ticket> ScannedTickets { get; set; } = new List<Ticket>();
        public ICollection<Event> OrganizedEvents { get; set; } = new List<Event>();
    }
}
