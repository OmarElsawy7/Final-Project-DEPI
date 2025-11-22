using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class Profile : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
