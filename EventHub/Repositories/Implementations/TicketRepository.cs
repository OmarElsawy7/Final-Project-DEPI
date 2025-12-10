using EventHub.Data;
using EventHub.Models;
using EventHub.Repositories.Interfaces;
using EventHub.ViewModels;
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

        public async Task<Ticket?> GetTicketWithEventAsync(int id)
        {
            return await _context.Tickets
                .Include(t => t.Event)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<CheckInHistoryViewModel>> GetCheckInHistoryAsync(string scannerId)
        {
            return await _context.Tickets
                .Where(t => t.ScannedByUserId == scannerId && t.CheckInTime != null)
                .OrderByDescending(t => t.CheckInTime)
                .Select(t => new CheckInHistoryViewModel
                {
                    TicketId = t.Id,
                    EventName = t.Event.Name,
                    BuyerName = t.Buyer.FullName,
                    Price = t.Event.Price,
                    CheckInTime = t.CheckInTime
                })
                .ToListAsync();
        }
    }
}
