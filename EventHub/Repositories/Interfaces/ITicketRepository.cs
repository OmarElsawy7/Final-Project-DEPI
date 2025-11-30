using EventHub.Models;

namespace EventHub.Repositories.Interfaces
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<Ticket?> GetByQrCodeAsync(string qrCodeValue);
        Task<IEnumerable<Ticket>> GetUserTicketsAsync(string userId);
    }
}
