using EventHub.Models;
using EventHub.Services.Interfaces;
using EventHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventHub.Controllers
{
    [Authorize(Roles = "Organizer")]
    public class DashboardController : Controller
    {
        private readonly IEventService _eventService;

        public DashboardController(IEventService eventService)
        {
            _eventService = eventService;
        }

        private string? GetCurrentUserId()
        {
            // Get logged-in user id from claims
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var events = await _eventService
                .FindAsync(e => e.OrganizerId == userId && !e.IsDeleted);

            return View(events.ToList()); // or use view model
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEvent(CreateAndEditEventViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var ev = new Event
            {
                Name = vm.Name,
                Date = vm.Date,
                Location = vm.Location,
                Category = vm.Category,
                Price = vm.Price,
                TotalTickets = vm.TotalTickets,
                AvailableTickets = vm.TotalTickets, // sync available with total on creation
                Description = vm.Description,
                OrganizerId = userId,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _eventService.CreateAsync(ev);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditEvent(int id)
        {
            var ev = await _eventService.GetByIdAsync(id);
            if (ev == null || ev.IsDeleted)
                return NotFound();

            var model = new CreateAndEditEventViewModel()
            {
                Id = ev.Id,
                Name = ev.Name,
                Date = ev.Date,
                Location = ev.Location,
                Category = ev.Category,
                Price = ev.Price,
                TotalTickets = ev.TotalTickets,
                Description = ev.Description
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEvent(CreateAndEditEventViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var ev = await _eventService.GetByIdAsync(vm.Id);
            if (ev == null || ev.IsDeleted)
                return NotFound();

            // Ensure that the current organizer owns this event
            if (ev.OrganizerId != userId)
                return Forbid();

            var boughtTickets = ev.TotalTickets - ev.AvailableTickets;
            // edit data 
            ev.Name = vm.Name;
            ev.Date = vm.Date;
            ev.Location = vm.Location;
            ev.Category = vm.Category;
            ev.Price = vm.Price;
            ev.TotalTickets = vm.TotalTickets;
            ev.AvailableTickets = vm.TotalTickets - boughtTickets;
            ev.Description = vm.Description;
            await _eventService.UpdateAsync(ev);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                return Challenge();

            var ev = await _eventService.GetByIdAsync(id);
            if (ev == null || ev.IsDeleted)
                return NotFound();

            // Ensure that the current organizer owns this event
            if (ev.OrganizerId != userId)
                return Forbid();

            // Soft delete
            ev.IsDeleted = true;
            await _eventService.UpdateAsync(ev);

            return RedirectToAction(nameof(Index));
        }
    }
}
