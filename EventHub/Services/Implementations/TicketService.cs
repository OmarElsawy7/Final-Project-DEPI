using EventHub.Models;
using EventHub.Repositories.Interfaces;
using EventHub.Services.Interfaces;
using EventHub.ViewModels;

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

        public async Task<Ticket?> GetTicketWithEventAsync(int id)
        {
            return await _ticketRepo.GetTicketWithEventAsync(id);
        }

        public async Task<IEnumerable<CheckInHistoryViewModel>> GetCheckInHistoryAsync(string scannerId)
        {
            return await _ticketRepo.GetCheckInHistoryAsync(scannerId);
        }
    }
}
