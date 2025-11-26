using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Event Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Display(Name = "Event Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Event Date is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Event Date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(200)]
        public string Location { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [DataType(DataType.MultilineText)] // Renders as a large TextArea in the form
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        [Range(0, 100000, ErrorMessage = "Price must be a positive number.")]
        [Column(TypeName = "decimal(18,2)")] // Essential for currency precision
        public decimal Price { get; set; }

        [Required]
        [Range(1, 100000, ErrorMessage = "Total Tickets must be specified.")]
        [Display(Name = "Total Tickets")]
        public int TotalTickets { get; set; }

        [Display(Name = "Available Tickets")]
        public int AvailableTickets { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // --- Foreign Key for Organizer ---
        // (Assuming you want to track which user created the event)
        [ForeignKey("Organizer")]
        public int? OrganizerId { get; set; } // Nullable if an admin creates it without a specific organizer

        // --- Navigation Properties ---
        // These are essential for the Dashboard to calculate statistics (e.g., Sold Tickets)
        public virtual User? Organizer { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public virtual ICollection<Pending> Pendings { get; set; } = new List<Pending>();
    }
}