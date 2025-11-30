using EventHub.Models;

namespace EventHub.Services.Interfaces
{
    public interface IEventService : IGenericService<Event>
    {
        Task<Event?> GetEventWithTicketsAsync(int id);
        Task<IEnumerable<Event>> GetActiveEventsAsync();
        Task SoftDeleteEventAsync(int id);
    }
}
