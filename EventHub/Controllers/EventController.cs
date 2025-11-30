using EventHub.Services.Interfaces;
using EventHub.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public IActionResult Index()
        {
            return View();  // No ViewModel because the events loads using AJAX Call.
        }

        [HttpGet]
        public async Task<IActionResult> Filter(string? search, string? category, string? priceFilter)
        {
            var events = await _eventService.GetActiveEventsAsync();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                string term = search.Trim().ToLower();
                events = events.Where(e =>
                    (e.Name != null && e.Name.ToLower().Contains(term)) ||
                    (e.Description != null && e.Description.ToLower().Contains(term)) ||
                    (e.Location != null && e.Location.ToLower().Contains(term))
                );
            }

            // Category
            if (!string.IsNullOrWhiteSpace(category) && category != "all")
            {
                events = events.Where(e =>
                    e.Category != null &&
                    e.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            // Price filter
            if (!string.IsNullOrWhiteSpace(priceFilter) && priceFilter != "all")
            {
                events = priceFilter switch
                {
                    "free" => events.Where(e => e.Price == 0),
                    "0-50" => events.Where(e => e.Price >= 0 && e.Price <= 50),
                    "50-100" => events.Where(e => e.Price > 50 && e.Price <= 100),
                    "100+" => events.Where(e => e.Price > 100),
                    _ => events
                };
            }

            var result = events
                .OrderBy(e => e.Date)
                .Select(e => new EventListItemViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Category = e.Category,
                    Price = e.Price,
                    Date = e.Date,
                    Location = e.Location,
                    Description = e.Description,
                    OrganizerName = e.Organizer.UserName ?? "Organizer",
                    AvailableTickets = e.AvailableTickets
                })
                .ToList();

            return Json(result);
        }
    }
}
