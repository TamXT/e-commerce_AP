using e_commerce_AP.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using e_commerce_AP.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_AP.Controllers
{
    public class BlogController : Controller
    {

        private readonly ECommerce_APDbContext _context;

        public BlogController(ECommerce_APDbContext context)
        {
            _context = context;
        }

        // GET: /Posts/
        public IActionResult Index()
        {
            var items = _context.TbPosts.ToList();
            return View(items);
        }

        // GET: /Posts/Detail/5
        public IActionResult Details(int id)
        {
            var item = _context.TbPosts.Find(id);
            if (item != null)
            {
                _context.SaveChanges();  // Chỉ cần save nếu có thay đổi
            }

            return View(item);
        }

    }
}
