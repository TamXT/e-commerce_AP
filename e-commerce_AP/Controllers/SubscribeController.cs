using Microsoft.AspNetCore.Mvc;
using e_commerce_AP.Data;
using e_commerce_AP.Models.EF;
using System.Threading.Tasks;

namespace e_commerce_AP.Controllers
{
    public class Subscribe : Controller
    {
        private readonly ECommerce_APDbContext _context;

        public Subscribe(ECommerce_APDbContext context)
        {
            _context = context;
        }

        // GET: Subscribe
        public IActionResult Index()
        {
            return View();
        }

        // POST: Subscribe/Create
        [HttpPost]
        public async Task<IActionResult> Create(TbSubscribe subscriber)
        {
            if (ModelState.IsValid)
            {
                await _context.TbSubscribes.AddAsync(subscriber);
                await _context.SaveChangesAsync();
                ViewBag.Message = "Đăng ký thành công!";
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Đăng ký thất bại!";
            return View(subscriber);
        }
    }
}
