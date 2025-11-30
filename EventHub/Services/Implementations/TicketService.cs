using EventHub.Models;
using EventHub.Repositories.Interfaces;
using EventHub.Services.Interfaces;

namespace EventHub.Services.Implementations
{
    public class TicketService : GenericService<Ticket>, ITicketService
    {
        private readonly ITicketRepository _ticketRepo;
        public TicketService(ITicketRepository ticketRepo) : base(ticketRepo)  // passing to generic base service
        {
            _ticketRepo = ticketRepo;
        }

        public async Task<Ticket?> GetByQrCodeAsync(string qrCode)
        {
            return await _ticketRepo.GetByQrCodeAsync(qrCode);
        }

        public async Task<IEnumerable<Ticket>> GetUserTicketsAsync(string userId)
        {
            return await _ticketRepo.GetUserTicketsAsync(userId);
        }
    }
}
