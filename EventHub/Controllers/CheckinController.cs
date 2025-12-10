using EventHub.Models;
using EventHub.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventHub.Controllers
{
    [Authorize(Roles = "Organizer")]
    public class CheckInController : Controller
    {
        private readonly ITicketService _ticketService;

        public CheckInController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        private string GetUserId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Handle scanned QR
        [HttpPost]
        public async Task<IActionResult> Verify(string qrToken)
        {
            if (string.IsNullOrWhiteSpace(qrToken))
                return Json(new { success = false, message = "Invalid QR Data." });

            var ticket = await _ticketService.GetByQrCodeAsync(qrToken);

            if (ticket == null)
                return Json(new { success = false, message = "Ticket not found." });

            var organizerId = GetUserId();

            if (ticket.Event.OrganizerId != organizerId)
                return Json(new { success = false, message = "This event is not yours." });

            if (ticket.CheckInTime != null)
            {
                return Json(new
                {
                    success = false,
                    status = "alreadyUsed",
                    message = "Ticket already checked in.",
                    ticketId = ticket.Id,
                    eventName = ticket.Event.Name,
                    holder = ticket.Buyer.Email,
                    price = ticket.Event.Price,
                    eventDate = ticket.Event.Date,
                    checkedInAt = ticket.CheckInTime
                });
            }

            ticket.CheckInTime = DateTime.UtcNow;
            ticket.ScannedByUserId = organizerId;
            ticket.Status = TicketStatus.CheckedIn;

            await _ticketService.UpdateAsync(ticket);

            return Json(new
            {
                success = true,
                message = "Check-in success",
                ticketId = ticket.Id,
                eventName = ticket.Event.Name,
                holder = ticket.Buyer.Email,
                price = ticket.Event.Price,
                eventDate = ticket.Event.Date,
                checkedInAt = ticket.CheckInTime
            });
        }

        // Get history
        [HttpGet]
        public async Task<IActionResult> History()
        {
            var organizerId = GetUserId();
            var history = await _ticketService.GetCheckInHistoryAsync(organizerId);

            return Json(history.Select(h => new
            {
                eventName = h.EventName,
                holder = h.BuyerName,
                ticketId = h.TicketId,
                price = h.Price,
                checkedInAt = h.CheckInTime
            }));
        }
    }
}
