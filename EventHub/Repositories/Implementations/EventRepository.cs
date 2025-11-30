using EventHub.Data;
using EventHub.Models;
using EventHub.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Repositories.Implementations
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Event>> GetActiveEventsAsync()
        {
            // Returns events that are not soft-deleted
            return await _context.Events
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public async Task<Event?> GetEventWithTicketsAsync(int id)
        {
            // Loads event with its tickets
            return await _context.Events
                .Include(e => e.Tickets)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
