using EventHub.Data;
using EventHub.Models;
using EventHub.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Repositories.Implementations
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Ticket?> GetByQrCodeAsync(string qrCodeValue)
        {
            // Returns ticket by QR code value
            return await _context.Tickets
                .Include(t => t.Event)
                .Include(t => t.Buyer)
                .FirstOrDefaultAsync(t => t.QrCodeValue == qrCodeValue);
        }

        public async Task<IEnumerable<Ticket>> GetUserTicketsAsync(string userId)
        {
            // Returns tickets bought by a specific user
            return await _context.Tickets
                .Include(t => t.Event)
                .Where(t => t.BuyerId == userId)
                .ToListAsync();
        }
    }
}
