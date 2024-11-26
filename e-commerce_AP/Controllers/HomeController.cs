using e_commerce_AP.Data;
using e_commerce_AP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using e_commerce_AP.Models.EF;

namespace e_commerce_AP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ECommerce_APDbContext _context; // Khai báo DbContext
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ECommerce_APDbContext context)
        {
            _logger = logger;
            _context = context;
        }



        public IActionResult Index()
        {
            var products = _context.TbProducts
             .Include(p => p.ProductCategory)
             .Include(p => p.TbProductImages)
             .ToList();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Subscribe(TbSubscribe model)
        {
            if (ModelState.IsValid)
            {
                // Lýu thông tin ðãng k? vào cõ s? d? li?u
                _context.TbSubscribes.Add(model);
                _context.SaveChanges();

                // Thông báo thành công
                TempData["SuccessMessage"] = "Subscription successful!";
            }

            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // Action ð? hi?n th? trang l?i
        [Route("Home/Error")]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("NotFound"); // Tr? v? view 404 n?u là l?i 404
            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
       }
   }
