using Microsoft.AspNetCore.Mvc;
using System.Linq;
using e_commerce_AP.Data;
using e_commerce_AP.Models.EF;

namespace e_commerce_AP.Components
{
    public class NewProductsViewComponent : ViewComponent
    {
        private readonly ECommerce_APDbContext _context;

        public NewProductsViewComponent(ECommerce_APDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int count = 20) // Số lượng sản phẩm hiển thị
        {
            // Lấy danh sách sản phẩm mới nhất
            var newProducts = _context.TbProducts
                .OrderByDescending(p => p.CreatedDate) // sử dụng trường CreateDate để xác định sản phẩm mới
                .Take(count)
                .ToList();

            return View(newProducts); // Trả về danh sách sản phẩm mới
        }
    }
}