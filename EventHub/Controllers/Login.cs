using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class Login : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
