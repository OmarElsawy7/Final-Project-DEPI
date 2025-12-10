using EventHub.Models;
using EventHub.Services.Interfaces;
using EventHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITicketService _ticketService;
        private readonly IWebHostEnvironment _env;

        public ProfileController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 ITicketService ticketService,
                                 IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _ticketService = ticketService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            // Get tickets for this user
            var tickets = await _ticketService.FindAsync(t => t.BuyerId == user.Id);

            var vm = new ProfileViewModel
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                UserType = (await _userManager.IsInRoleAsync(user, "Organizer"))
                            ? "Event Organizer"
                            : "Customer",
                ProfilePhoto = user.ProfilePhotoPath,
                TicketsCount = tickets.Count()
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            var vm = new EditProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                CurrentPhotoPath = user.ProfilePhotoPath
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProfileViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Challenge();

            // Update basic fields
            user.FullName = vm.FullName;
            user.Email = vm.Email;

            // Upload profile photo (optional)
            if (vm.NewPhoto != null)
            {
                string uploadsRoot = Path.Combine(_env.WebRootPath, "uploads/profile");

                if (!Directory.Exists(uploadsRoot))
                    Directory.CreateDirectory(uploadsRoot);

                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(vm.NewPhoto.FileName)}";
                string fullPath = Path.Combine(uploadsRoot, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await vm.NewPhoto.CopyToAsync(stream);
                }

                // Save relative path in DB
                user.ProfilePhotoPath = "/uploads/profile/" + fileName;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var e in result.Errors)
                    ModelState.AddModelError("", e.Description);

                return View(vm);
            }

            // ----------------------
            // Sync FullName claim
            // ----------------------
            try
            {
                // Get existing claims for the user
                var claims = await _userManager.GetClaimsAsync(user);
                var existing = claims.FirstOrDefault(c => c.Type == "FullName");

                var newClaim = new System.Security.Claims.Claim("FullName", user.FullName ?? string.Empty);

                if (existing != null)
                {
                    // Replace existing claim (keeps claim id/relationships clean)
                    await _userManager.ReplaceClaimAsync(user, existing, newClaim);
                }
                else
                {
                    // Add claim if it wasn't present
                    await _userManager.AddClaimAsync(user, newClaim);
                }

                // Refresh cookie so current session contains the updated claim
                await _signInManager.RefreshSignInAsync(user);
            }
            catch
            {
                // If claim update fails we don't block the user—log in real app
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
