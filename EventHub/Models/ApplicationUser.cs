using Microsoft.AspNetCore.Identity;

namespace EventHub.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = null!;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public string? ProfilePhotoPath { get; set; }


        // Navigation
        public ICollection<Ticket> BoughtTickets { get; set; } = new List<Ticket>();
        public ICollection<Ticket> ScannedTickets { get; set; } = new List<Ticket>();
        public ICollection<Event> OrganizedEvents { get; set; } = new List<Event>();
    }
}
