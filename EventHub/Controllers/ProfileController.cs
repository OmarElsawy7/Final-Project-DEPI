using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
