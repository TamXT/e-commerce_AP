using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using e_commerce_AP.Data;
using e_commerce_AP.Models.EF;

namespace e_commerce_AP.Components
{
    public class SubscribeViewComponent : ViewComponent
    {
        private readonly ECommerce_APDbContext _context;

        public SubscribeViewComponent(ECommerce_APDbContext context)
        {
            _context = context;
        }

        // Phương thức để hiển thị view đăng ký
        public IViewComponentResult Invoke()
        {
            // Trả về view mặc định
            return View("Default", new TbSubscribe());
        }

        [HttpPost]
        public async Task<IViewComponentResult> Subscribe(TbSubscribe model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreatedDate = DateTime.Now; // Đặt giá trị ngày hiện tại
                    await _context.TbSubscribes.AddAsync(model);
                    await _context.SaveChangesAsync();

                    // Lưu thông báo thành công vào TempData
                    TempData["SuccessMessage"] = "Đăng ký thành công!";
                }
                catch (Exception)
                {
                    // Lưu thông báo lỗi vào TempData
                    TempData["ErrorMessage"] = "Đăng ký không thành công. Vui lòng thử lại.";
                }
            }
            else
            {
                // Lưu thông báo lỗi vào TempData
                TempData["ErrorMessage"] = "Thông tin không hợp lệ. Vui lòng kiểm tra lại.";
            }

            // Trả về view mặc định cùng mô hình
            return View("Default", model);
        }
    }
}