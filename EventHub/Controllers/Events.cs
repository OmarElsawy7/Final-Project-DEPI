using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class Events : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
