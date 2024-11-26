using e_commerce_AP.Data;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_AP.Components
{

    
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ECommerce_APDbContext _context;

        public CategoryViewComponent(ECommerce_APDbContext context)
        {
            _context = context;
        }

        // Phương thức chính của View Component
        public IViewComponentResult Invoke()
        {
            // Lấy danh sách danh mục sản phẩm
            var categories = _context.TbProductCategories.ToList();

            return View(categories); // Trả về danh sách danh mục
        }
    }
}
