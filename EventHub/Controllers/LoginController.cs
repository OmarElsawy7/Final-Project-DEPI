using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
