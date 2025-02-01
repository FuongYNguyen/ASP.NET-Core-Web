using KyNiem50NamWeb.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KyNiem50NamWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate(string Username, string Password)
        {
            // Kiểm tra sự tồn tại của tài khoản
            var account = _context.User.SingleOrDefault(tk => tk.Username == Username && tk.Password == Password);
            if (account != null)
            {
                // Lưu thông tin vào Session
                HttpContext.Session.SetString("username", account.Username);
                HttpContext.Session.SetInt32("id", account.UserID);

                // Lưu dữ liệu tạm thời (TempData)
                TempData["username"] = account.Username;
                TempData["password"] = account.Password;

                // Chuyển hướng đến trang 
                return RedirectToAction("Index", "Admin");

            }
            else
            {
                // Nếu thông tin không đúng, ở lại trang login và hiển thị thông báo lỗi
                ViewBag.ErrorMessage = "Số điện thoại hoặc mật khẩu không đúng. Vui lòng thử lại.";
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();  // Xóa toàn bộ session
            return RedirectToAction("Index", "Home");  // Quay lại trang chủ
        }

    }
}
