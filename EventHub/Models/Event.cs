using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EventHub.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [StringLength(100)]
        public string? Category { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, StringLength(200)]
        public string Location { get; set; } = null!;

        [Precision(18, 2)]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int TotalTickets { get; set; }

        [Range(0, int.MaxValue)]
        public int AvailableTickets { get; set; }

        // Organizer
        [Required]
        public string OrganizerId { get; set; } = null!;
        public ApplicationUser Organizer { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
