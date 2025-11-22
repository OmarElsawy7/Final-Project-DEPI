namespace EventHub.Models
{
    public class CheckIn
    {
        public int CheckInID { get; set; }
        public int EventID { get; set; }
        public int TicketID { get; set; }
        public DateTime CheckInTime { get; set; }
        public string ScannedBy { get; set; }

        // Navigation Properties
        public Event Event { get; set; }
        public Ticket Ticket { get; set; }
    }

}
