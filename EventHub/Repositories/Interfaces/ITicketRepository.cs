using EventHub.Models;
using EventHub.ViewModels;

namespace EventHub.Repositories.Interfaces
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<Ticket?> GetByQrCodeAsync(string qrCodeValue);
        Task<IEnumerable<Ticket>> GetUserTicketsAsync(string userId);
        Task<Ticket?> GetTicketWithEventAsync(int id);
        Task<IEnumerable<CheckInHistoryViewModel>> GetCheckInHistoryAsync(string scannerId);
    }
}
