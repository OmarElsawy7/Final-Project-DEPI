namespace EventHub.ViewModels
{
    public class EventListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Category { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public string? Description { get; set; }
        public string OrganizerName { get; set; } = null!;
        public int AvailableTickets { get; set; }
    }
}
