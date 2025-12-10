using EventHub.Models;
using EventHub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventHub.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var vm = new AuthWrapperViewModel();
            return View("Index", vm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken] // for better security
        public async Task<IActionResult> Register([Bind(Prefix = "Register")] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var vm = new AuthWrapperViewModel
                {
                    Login = new LoginViewModel(),
                    Register = model,
                    ActiveTab = "register"
                };
                return View("Index", vm);
            }

            string roleName = model.UserType;

            var user = new ApplicationUser
            {
                FullName = model.FullName,
                UserName = model.FullName.Split(" ")[0],
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);

                var vmFail = new AuthWrapperViewModel { Login = new LoginViewModel(), Register = model, ActiveTab = "register" };
                return View("Index", vmFail);
            }

            await _userManager.AddToRoleAsync(user, roleName);
            // add FullName Claim
            var claim = new Claim("FullName", user.FullName ?? "");
            await _userManager.AddClaimAsync(user, claim);
            // sign in
            await _signInManager.SignInAsync(user, isPersistent: false);

            // Redirect by role
            if (model.UserType == "Organizer")
                return RedirectToAction("Index", "Dashboard");

            return RedirectToAction("Index", "Event");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login([Bind(Prefix ="Login")] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var vm = new AuthWrapperViewModel
                {
                    Login = model,
                    Register = new RegisterViewModel(),
                    ActiveTab = "login"
                };
                return View("Index", vm);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                var vmFail = new AuthWrapperViewModel { Login = model, Register = new RegisterViewModel(), ActiveTab = "login" };
                return View("Index", vmFail);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                var vmFail = new AuthWrapperViewModel { Login = model, Register = new RegisterViewModel(), ActiveTab = "login" };
                return View("Index", vmFail);
            }

            string requiredRole = model.UserType;

            if (!await _userManager.IsInRoleAsync(user, requiredRole))
            {
                await _signInManager.SignOutAsync();
                ModelState.AddModelError("", "Wrong role selected");
                var vmFail = new AuthWrapperViewModel { Login = model, Register = new RegisterViewModel(), ActiveTab = "login" };
                return View("Index", vmFail);
            }

            // Redirect by role
            if (model.UserType == "Organizer")
                return RedirectToAction("Index", "Dashboard");

            return RedirectToAction("Index", "Event");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
