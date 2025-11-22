using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class Payment : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
