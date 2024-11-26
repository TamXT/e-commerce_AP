using Microsoft.AspNetCore.Mvc;
using e_commerce_AP.Data;

namespace e_commerce_AP.Components
{
    public class Partial_ProductViewComponent : ViewComponent
    {
        private readonly ECommerce_APDbContext _context;

        public Partial_ProductViewComponent(ECommerce_APDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var sanPham = await _context.TbProducts.FindAsync(id); // Giả sử bạn có một bảng sản phẩm
            return View(sanPham);
        }
    }
}