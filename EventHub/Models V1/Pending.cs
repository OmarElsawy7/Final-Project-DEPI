using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.ModelsV1
{
    public class Pending
    {
        [Key]
        public int PurchaseID { get; set; }

        // --- Foreign Key for Event ---
        [Required]
        [ForeignKey("Event")]
        public int EventID { get; set; }

        // --- Foreign Key for Buyer ---
        // Added BuyerId to properly link the User table in the database
        [ForeignKey("Buyer")]
        public int BuyerId { get; set; }

        [Required(ErrorMessage = "Buyer name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Display(Name = "Buyer Name")]
        public string BuyerName { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [Display(Name = "Buyer Email")]
        public string BuyerEmail { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "Invalid amount.")]
        [Column(TypeName = "decimal(18,2)")] // Essential for currency precision in SQL Server
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        // --- Navigation Properties ---
        public virtual Event? Event { get; set; }
        public virtual User? Buyer { get; set; }
    }
}