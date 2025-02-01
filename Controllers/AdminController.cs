using Microsoft.AspNetCore.Mvc;
using KyNiem50NamWeb.Repository;
using KyNiem50NamWeb.Models;

namespace KyNiem50NamWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly DataContext _context;
        public AdminController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var posts = _context.Post.ToList();
            return View(posts);
        }
    }
}
