using System.ComponentModel.DataAnnotations;

namespace EventHub.ViewModels
{
    public class PaymentViewModel
    {
        // Event info
        [Required]
        public int EventId { get; set; }
        public string EventName { get; set; } = null!;
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = null!;
        public decimal UnitPrice { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        public decimal TotalAmount => UnitPrice * Quantity;

        
        // Payment form fields (from the payment page)
        [Required]
        public string CardholderName { get; set; } = null!;

        [Required]
        //[CreditCard]
        public string CardNumber { get; set; } = null!;

        [Required]
        public string ExpiryDate { get; set; } = null!;

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string CVV { get; set; } = null!;

        [Required, EmailAddress]
        public string BillingEmail { get; set; } = null!;
    }
}
