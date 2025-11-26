using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
