using System.ComponentModel.DataAnnotations;

namespace EventHub.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        // relation with Event
        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        // buyer (User)
        [Required]
        public string BuyerId { get; set; } = null!;
        public ApplicationUser Buyer { get; set; } = null!;

        //  QR value (Token)
        [Required, StringLength(100)]
        public string QrCodeValue { get; set; } = null!;

        // ticket status
        [Required]
        public TicketStatus Status { get; set; } = TicketStatus.PendingPayment;

        // buy data
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        [StringLength(50)]
        public string? PaymentMethod { get; set; }    // "PayPal"

        [StringLength(200)]
        public string? PaymentReference { get; set; } // PayPal OrderId / CaptureId

        // Check-in data
        public DateTime? CheckInTime { get; set; }

        public string? ScannedByUserId { get; set; }
        public ApplicationUser? ScannedByUser { get; set; }
    }
}
