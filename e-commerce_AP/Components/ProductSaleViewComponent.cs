using Microsoft.AspNetCore.Mvc;
using System.Linq;
using e_commerce_AP.Data;
using e_commerce_AP.Models.EF;

namespace e_commerce_AP.Components
{
    public class ProductSaleViewComponent : ViewComponent
    {
        private readonly ECommerce_APDbContext _context;

        public ProductSaleViewComponent(ECommerce_APDbContext context)
        {
            _context = context;
        }

        // Phương thức chính của View Component
        public IViewComponentResult Invoke(int topCount = 10)
        {
            // Lấy danh sách 10 sản phẩm có giá rẻ nhất
            var cheapProducts = _context.TbProducts
                .OrderBy(p => p.PriceSale > 0 ? p.PriceSale : p.Price) // Sắp xếp theo giá sale nếu có, nếu không thì theo giá gốc
                .Take(topCount)
                .ToList();

            return View(cheapProducts); // Trả về danh sách sản phẩm có giá rẻ nhất
        }
    }
}