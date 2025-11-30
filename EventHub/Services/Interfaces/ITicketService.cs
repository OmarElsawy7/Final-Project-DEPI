using EventHub.Models;

namespace EventHub.Services.Interfaces
{
    public interface ITicketService : IGenericService<Ticket>
    {
        Task<Ticket?> GetByQrCodeAsync(string qrCode);
        Task<IEnumerable<Ticket>> GetUserTicketsAsync(string userId);
    }
}
