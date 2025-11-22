using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class Ticket : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
