using EventHub.Models;
using EventHub.ViewModels;

namespace EventHub.Services.Interfaces
{
    public interface ITicketService : IGenericService<Ticket>
    {
        Task<Ticket?> GetByQrCodeAsync(string qrCode);
        Task<IEnumerable<Ticket>> GetUserTicketsAsync(string userId);
        Task<Ticket?> GetTicketWithEventAsync(int id);
        Task<IEnumerable<CheckInHistoryViewModel>> GetCheckInHistoryAsync(string scannerId);
    }
}
