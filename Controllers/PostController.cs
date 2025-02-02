using Microsoft.AspNetCore.Mvc;
using KyNiem50NamWeb.Repository;
using KyNiem50NamWeb.Models;
using System.Linq;
using System;
using Microsoft.IdentityModel.Tokens;

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

        //get 
        [HttpGet]
        [Route("Post/GetPost")]
        public IActionResult GetPost(int id, bool isDetail = false)
        {
            var post = _context.Post.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            if (isDetail)
            {
                // Trả về View postDetail cho trang chi tiết bài viết
                return View("DetailPost", post);
            }
            else
            {
                // Trả về JSON cho trang quản lý bài viết
                return Json(post);
            }
        }

        // add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Post post)
        {
            post.CreatedDate = DateTime.Now;
            string? username = HttpContext.Session.GetString("username");
            int? id = HttpContext.Session.GetInt32("id");
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(id.ToString()))
            {
                ModelState.AddModelError("", "Bạn chưa đăng nhập.");
                return RedirectToAction("Index", "Admin");
            }

            post.CreatedBy = username;
            post.CreatedByUserID = (int)id; // Cần thay thế bằng UserID thật từ session/auth

            try
            {
                _context.Post.Add(post);
                _context.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi khi lưu vào cơ sở dữ liệu: {ex.Message}");
                return RedirectToAction("Index", "Admin");
            }
        }
        //mod
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModifyPost(int id, Post modifiedPost)
        {
            var existingPost = _context.Post.FirstOrDefault(p => p.Id == id);

            if (existingPost == null)
            {
                return NotFound(); 
            }

            existingPost.Title = modifiedPost.Title;
            existingPost.ContentPost = modifiedPost.ContentPost;
            existingPost.Type = modifiedPost.Type;
            existingPost.ModifiedDate = DateTime.Now;

            string? username = HttpContext.Session.GetString("username");
            int? userId = HttpContext.Session.GetInt32("id");
            if (string.IsNullOrEmpty(username) || userId == null)
            {
                ModelState.AddModelError("", "Bạn chưa đăng nhập.");
                return RedirectToAction("Index", "Admin");
            }

            existingPost.ModifiedBy = username;
            existingPost.ModifiedByUserID = (int)userId;

            try
            {
                _context.Post.Update(existingPost);
                _context.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi khi lưu vào cơ sở dữ liệu: {ex.Message}");
                return RedirectToAction("Index", "Admin");
            }
        }

        //delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var post = _context.Post.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound(); 
            }

            try
            {
                _context.Post.Remove(post);
                _context.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi khi xóa bài viết: {ex.Message}");
                return RedirectToAction("Index", "Admin");
            }
        }


    }
}
