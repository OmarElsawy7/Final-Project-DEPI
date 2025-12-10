using EventHub.Models;
using EventHub.Services.Interfaces;
using EventHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventHub.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // Helper: Get logged-in user id
        private string? GetUserId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier);

        [HttpGet]
        public async Task<IActionResult> GetMyTickets(string filter = "all")
        {
            string? userId = GetUserId();
            if (userId == null) return Challenge();

            var tickets = await _ticketService.GetUserTicketsAsync(userId);

            var now = DateTime.UtcNow;

            if (filter == "upcoming")
                tickets = tickets.Where(t => t.Event.Date >= now).ToList();
            else if (filter == "past")
                tickets = tickets.Where(t => t.Event.Date < now).ToList();

            var result = tickets.Select(t => new
            {
                ticketId = t.Id,
                eventName = t.Event.Name,
                eventLocation = t.Event.Location,
                eventDate = t.Event.Date,
                price = t.Event.Price,
                used = t.CheckInTime != null,
                qrCode = t.QrCodeValue
            });

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketService.GetTicketWithEventAsync(id);
            if (ticket == null) return NotFound();

            if (ticket.BuyerId != GetUserId())
                return Forbid();

            var vm = new TicketDisplayViewModel
            {
                TicketId = ticket.Id,
                EventName = ticket.Event.Name,
                EventDate = ticket.Event.Date,
                EventLocation = ticket.Event.Location,
                QrCodeValue = ticket.QrCodeValue,
                Price = ticket.Event.Price,
                PurchaseDate = ticket.PurchaseDate,
                IsUsed = ticket.CheckInTime != null,
                IsPast = ticket.Event.Date < DateTime.UtcNow
            };

            ViewBag.FullName = User.FindFirst("FullName")?.Value;

            return View(vm);
        }

        // 1) Show tickets for current user (All)
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = GetUserId();
            if (userId == null) return Challenge();

            var tickets = await _ticketService.GetUserTicketsAsync(userId);

            var vm = tickets.Select(t => new TicketDisplayViewModel
            {
                TicketId = t.Id,
                EventName = t.Event.Name,
                EventLocation = t.Event.Location,
                EventDate = t.Event.Date,
                Price = t.Event.Price,
                QrCodeValue = t.QrCodeValue,
                PurchaseDate = t.PurchaseDate,
                IsUsed = t.CheckInTime != null,
                IsPast = t.Event.Date < DateTime.UtcNow
            }).ToList();

            return View(vm);
        }
    }
}
