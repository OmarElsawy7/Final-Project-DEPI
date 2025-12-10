namespace EventHub.ViewModels
{
    public class TicketDisplayViewModel
    {
        public int TicketId { get; set; }
        public string EventName { get; set; } = null!;
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; } = null!;
        public decimal Price { get; set; }
        public string QrCodeValue { get; set; } = null!;
        public DateTime PurchaseDate { get; set; }
        public bool IsUsed { get; set; }
        public bool IsPast { get; set; }
    }
}
