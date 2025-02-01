using KyNiem50NamWeb.App_Start;
using KyNiem50NamWeb.Models;
using KyNiem50NamWeb.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KyNiem50NamWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        // Action để hiển thị danh sách bài viết theo type
       
        public IActionResult Index()
        {
            // Kiểm tra session trong action method
            var username = HttpContext.Session.GetString("username");

            // Nếu có session, điều hướng đến trang Admin
            if (username != null)
            {
                return RedirectToAction("Index", "Admin");  // Điều hướng đến Admin
            }

            string type = "5";
            try
            {
                var posts = _context.Post
                    .Where(p => p.Type != null && p.Type == type)
                    .OrderByDescending(p => p.CreatedDate)  // Sắp xếp theo ngày tạo, mới nhất ở trên
                    .Take(3)                                // Lấy 3 bản ghi đầu tiên
                    .ToList();
                return View(posts);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return View(new List<Post>());
            }
        }

    }
}
