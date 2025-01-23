using KyNiem50NamWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KyNiem50NamWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
       
    }
}
