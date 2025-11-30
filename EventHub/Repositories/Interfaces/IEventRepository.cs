using EventHub.Models;

namespace EventHub.Repositories.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<Event?> GetEventWithTicketsAsync(int id);
        Task<IEnumerable<Event>> GetActiveEventsAsync();
    }
}
