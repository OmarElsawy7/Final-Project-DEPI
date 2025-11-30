using EventHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Event - Organizer (User)
            builder.Entity<Event>()
                .HasOne(e => e.Organizer)
                .WithMany(u => u.OrganizedEvents)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Event - Tickets (One-to-Many)
            builder.Entity<Event>()
                .HasMany(e => e.Tickets)
                .WithOne(t => t.Event)
                .HasForeignKey(t => t.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket - Buyer (User)
            builder.Entity<Ticket>()
                .HasOne(t => t.Buyer)
                .WithMany(u => u.BoughtTickets)
                .HasForeignKey(t => t.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket - ScannedByUser (User - optional)
            builder.Entity<Ticket>()
                .HasOne(t => t.ScannedByUser)
                .WithMany(u => u.ScannedTickets)
                .HasForeignKey(t => t.ScannedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
