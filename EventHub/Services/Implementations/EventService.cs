using EventHub.Models;
using EventHub.Repositories.Interfaces;
using EventHub.Services.Interfaces;

namespace EventHub.Services.Implementations
{
    public class EventService : GenericService<Event>, IEventService
    {
        private readonly IEventRepository _eventRepo;
        public EventService(IEventRepository eventRepo) : base(eventRepo)
        {
            _eventRepo = eventRepo;
        }

        public async Task<IEnumerable<Event>> GetActiveEventsAsync()
        {
            return await _eventRepo.GetActiveEventsAsync();
        }

        public async Task<Event?> GetEventWithTicketsAsync(int id)
        {
            return await _eventRepo.GetEventWithTicketsAsync(id);
        }

        public async Task SoftDeleteEventAsync(int id)
        {
            var ev = await _eventRepo.GetByIdAsync(id);
            if (ev is null) return;

            ev.IsDeleted = true;
            _eventRepo.Update(ev);
            await _eventRepo.SaveAsync();
        }
    }
}
