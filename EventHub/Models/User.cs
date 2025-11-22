namespace EventHub.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime JoinedDate { get; set; }

        // Navigation Properties
        public ICollection<Event> Events { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Pending> Pendings { get; set; }
    }

}
