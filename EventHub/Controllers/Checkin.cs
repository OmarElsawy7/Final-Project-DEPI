using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class Checkin : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
