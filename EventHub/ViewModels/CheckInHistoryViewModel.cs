namespace EventHub.ViewModels
{
    public class CheckInHistoryViewModel
    {
        public int TicketId { get; set; }
        public string EventName { get; set; } = null!;
        public string BuyerName { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime? CheckInTime { get; set; }
    }
}
