using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventHub.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(150)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [StringLength(50)]
        public string Role { get; set; } // Example values: "Admin", "Organizer", "Customer"

        [DataType(DataType.DateTime)]
        [Display(Name = "Date Joined")]
        public DateTime JoinedDate { get; set; } = DateTime.Now; // Default to current date

        // --- Navigation Properties ---
        // Initializing lists to avoid "Null Reference" errors
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public virtual ICollection<Pending> Pendings { get; set; } = new List<Pending>();
    }
}