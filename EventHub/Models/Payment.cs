namespace EventHub.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int TicketID { get; set; }
        public string CardHolder { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string CVV { get; set; }

        // Navigation Properties
        public Ticket Ticket { get; set; }
    }

}
