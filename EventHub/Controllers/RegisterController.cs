using EventHub.Models;
using EventHub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Login"); // in shared folder
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken] // for better security
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Login", model);

            string roleName = model.UserType == "organizer" ? "Organizer" : "Customer";

            var user = new ApplicationUser
            {
                UserName = model.FullName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);

                return View("Login", model);
            }

            await _userManager.AddToRoleAsync(user, roleName);

            // Redirect by role
            if (model.UserType == "organizer")
                return RedirectToAction("Index", "Dashboard");

            return RedirectToAction("Index", "Event");
        }
    }
}
