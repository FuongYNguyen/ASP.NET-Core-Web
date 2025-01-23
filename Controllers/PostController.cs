using Microsoft.AspNetCore.Mvc;
using KyNiem50NamWeb.Repository;
using KyNiem50NamWeb.Models;
using System.Linq;
using System;

namespace KyNiem50NamWeb.Controllers
{
    public class PostController : Controller
    {
        private readonly DataContext _context;

        public PostController(DataContext context)
        {
            _context = context;
        }

        // Action để hiển thị danh sách bài viết theo type
        public IActionResult ListPost(int type)
        {
            // Lọc các bài viết có type = ?
            var posts = _context.Post.Where(p => p.Type == type.ToString()).ToList();
            // 1: Con Nguoi, 2: Kiene thuc, 3: Khon vien truong, 4: Hoạt động, 5: cam nhan 
            // Trả về view và truyền danh sách bài viết
            return View(posts);
        }
        public IActionResult DetailPost(int id)
        {
            // Lấy bài viết theo Id
            var post = _context.Post.FirstOrDefault(p => p.Id == id);
           

            // Kiểm tra nếu bài viết không tồn tại
            if (post == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy bài viết
            }

            // Trả về view và truyền bài viết
            return View(post);
        }

    }
}
