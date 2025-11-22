namespace EventHub.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int EventID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
        public string HolderName { get; set; }
        public string HolderEmail { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PaymentMethod { get; set; }
        public bool Used { get; set; }

        // Navigation Properties
        public Event Event { get; set; }
        public ICollection<CheckIn> CheckIns { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }

}
