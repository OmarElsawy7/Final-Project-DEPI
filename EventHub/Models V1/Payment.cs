using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.ModelsV1
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        [Required]
        [ForeignKey("Ticket")]
        public int TicketID { get; set; }

        [Required(ErrorMessage = "Card holder name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Display(Name = "Card Holder Name")]
        public string CardHolder { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "Invalid amount.")]
        [Column(TypeName = "decimal(18,2)")] // Ensures correct currency storage in SQL Server
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Expiration date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Expiration Date")]
        public DateTime ExpiredDate { get; set; }

        [Required(ErrorMessage = "CVV security code is required.")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "CVV must be 3 or 4 digits.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers are allowed.")]
        public string CVV { get; set; }

        // Navigation Properties
        public virtual Ticket? Ticket { get; set; }
    }
}