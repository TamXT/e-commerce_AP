using Microsoft.AspNetCore.Mvc;
using e_commerce_AP.Data;
using e_commerce_AP.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_AP.Components
{
    public class PostsViewComponent : ViewComponent // Kế thừa từ ViewComponent thay vì Controller
    {
        private readonly ECommerce_APDbContext _context;

        public PostsViewComponent(ECommerce_APDbContext context)
        {
            _context = context;
        }

        // Phương thức InvokeAsync để lấy dữ liệu và trả về view
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var posts = await _context.TbPosts.ToListAsync(); // Lấy danh sách bài viết từ database
            return View("Default", posts); // Trả về view "Default" với danh sách bài viết
        }
    }
}