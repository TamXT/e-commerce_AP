using e_commerce_AP.Data;
using e_commerce_AP.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_AP.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ECommerce_APDbContext _context;

        public CategoryController(ECommerce_APDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string SeoTitle = "")
        {
            TbProductCategory category = _context.TbProductCategories.Where(c => c.SeoTitle == SeoTitle).FirstOrDefault();

            if (category == null) return RedirectToAction("Index");

            var productsByCategory = _context.TbProducts.Where(p => p.ProductCategoryId == category.Id);

            return View(await productsByCategory.OrderByDescending(p => p.Id).ToListAsync());
        }
    }
}
