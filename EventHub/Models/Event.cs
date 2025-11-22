namespace EventHub.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int TotalTickets { get; set; }
        public int AvailableTickets { get; set; }
        public int OrganizerId { get; set; }
        public string OrganizerName { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public User Organizer { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Pending> Pendings { get; set; }
    }

}
