using EventHub.Models;
using EventHub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Login"); // in shared folder
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Login", model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View("Login", model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View("Login", model);
            }

            string requiredRole = model.UserType == "organizer" ? "Organizer" : "Customer";

            if (!await _userManager.IsInRoleAsync(user, requiredRole))
            {
                await _signInManager.SignOutAsync();
                ModelState.AddModelError("", "Wrong role selected");
                return View("Login", model);
            }

            // Redirect by role
            if (model.UserType == "organizer")
                return RedirectToAction("Index", "Dashboard");

            return RedirectToAction("Index", "Event");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
