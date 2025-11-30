using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class CheckinController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
