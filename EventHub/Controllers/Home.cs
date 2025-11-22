using System.Diagnostics;
using EventHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
