using EventHub.Models;
using EventHub.Services.Interfaces;
using EventHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;

namespace EventHub.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IEventService _eventService;
        private readonly ITicketService _ticketService;

        public PaymentController(IEventService eventService, 
            ITicketService ticketService)
        {
            _eventService = eventService;
            _ticketService = ticketService;
        }

        private string? GetUserId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier);

        [HttpGet]
        public async Task<IActionResult> Purchase(int eventId, int quantity = 1)
        {
            if (quantity < 1)
                quantity = 1;

            var ev = await _eventService.GetByIdAsync(eventId);
            if (ev == null || ev.IsDeleted)
                return NotFound();

            if (ev.AvailableTickets <= 0)
                return BadRequest("No tickets available for this event.");

            if (quantity > ev.AvailableTickets)
                quantity = ev.AvailableTickets; // clamp

            var vm = new PaymentViewModel
            {
                EventId = ev.Id,
                EventName = ev.Name,
                EventDate = ev.Date,
                Location = ev.Location,
                UnitPrice = ev.Price,
                Quantity = quantity
            };

            ViewBag.FullName = User.FindFirst("FullName")?.Value;
            return View("Payment", vm); // Views/Payment/Payment.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Purchase(PaymentViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Payment", model);

            string? userId = GetUserId();
            if (userId == null) return Challenge();

            var ev = await _eventService.GetByIdAsync(model.EventId);
            if (ev == null || ev.IsDeleted)
                return NotFound();

            if (model.Quantity < 1)
                model.Quantity = 1;

            if (model.Quantity > ev.AvailableTickets)
            {
                ModelState.AddModelError("", $"Only {ev.AvailableTickets} tickets are available.");
                
                // In-case price changed in DB
                model.UnitPrice = ev.Price;
                return View("Payment", model);
            }

            // ============================
            // Payment integration here  (PayPal/Stripe...)
            // This is a (Simulation) only
            // ============================

            // Ticket Creation
            for (int i = 0; i < model.Quantity; i++)
            {
                string qrToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(20));

                var ticket = new Ticket
                {
                    EventId = ev.Id,
                    BuyerId = userId,
                    QrCodeValue = qrToken,
                    Status = TicketStatus.Paid,
                    PurchaseDate = DateTime.UtcNow,
                    PaymentMethod = "Card",
                    PaymentReference = Guid.NewGuid().ToString()
                };

                await _ticketService.CreateAsync(ticket);
            }

            ev.AvailableTickets -= model.Quantity;
            await _eventService.UpdateAsync(ev);

            return RedirectToAction("Index", "Profile");  // You Can make ticket page
        }
    }
}
