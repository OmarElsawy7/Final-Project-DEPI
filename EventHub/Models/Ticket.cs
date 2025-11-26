using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        // --- Foreign Key for Event ---
        [Required]
        [ForeignKey("Event")]
        public int EventID { get; set; }

        // Note: Storing EventName, Date, and Location here creates "Redundant Data" 
        // since we already have the EventID. However, it can be useful as a "Snapshot" 
        // in case the event details change later.

        [Display(Name = "Event Name")]
        public string? EventName { get; set; } // Made nullable as it can be fetched from Event relation

        [DataType(DataType.DateTime)]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [Display(Name = "Event Location")]
        public string? EventLocation { get; set; }

        // --- Holder Information ---
        [Required(ErrorMessage = "Holder Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Display(Name = "Holder Name")]
        public string HolderName { get; set; }

        [Required(ErrorMessage = "Holder Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [Display(Name = "Holder Email")]
        public string HolderEmail { get; set; }

        [Required]
        [Range(0, 100000, ErrorMessage = "Price cannot be negative.")]
        [Column(TypeName = "decimal(18,2)")] // Essential for currency precision in SQL Server
        public decimal Price { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PurchaseDate { get; set; } = DateTime.Now; // Default to current time

        [Required(ErrorMessage = "Payment Method is required.")]
        [StringLength(50)]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Used (Checked-In)")]
        public bool Used { get; set; } = false; // Default is false until they check in

        // --- Navigation Properties ---
        public virtual Event? Event { get; set; }
        public virtual ICollection<CheckIn> CheckIns { get; set; } = new List<CheckIn>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}