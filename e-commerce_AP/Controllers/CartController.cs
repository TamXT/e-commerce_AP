﻿using e_commerce_AP.Data;
using e_commerce_AP.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;


namespace e_commerce_AP.Controllers
{
    public class CartController : Controller
    {
        private readonly ECommerce_APDbContext _context;

        public CartController(ECommerce_APDbContext context)
        {
            _context = context;
        }

        // Hiển thị giỏ hàng
        public IActionResult Index()
        {
            // Lấy tất cả các đơn hàng có chi tiết từ cơ sở dữ liệu
            var cartItems = _context.TbOrders
                .Include(o => o.TbOrderDetails) // Giả sử rằng TbOrder có một mối quan hệ với TbOrderDetail
                    .ThenInclude(d => d.Product) // Giả sử TbOrderDetail có một mối quan hệ với TbProduct
                .ToList();

            // Lấy tổng số lượng sản phẩm trong giỏ hàng và lưu vào ViewBag
            ViewBag.CartItemCount = GetCartItemCount();

            var orders = _context.TbOrders.ToList(); // Lấy danh sách đơn hàng
            decimal totalAmount = orders.Sum(o => o.TotalAmount);
            return View(cartItems);
        }

        // Hiển thị form thêm sản phẩm vào giỏ hàng
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ClearCart()
        {
            // Lấy tất cả các đơn hàng trong cơ sở dữ liệu
            var orders = _context.TbOrders
                .Include(o => o.TbOrderDetails) // Bao gồm chi tiết đơn hàng
                .ToList();

            // Xóa tất cả chi tiết đơn hàng và các đơn hàng
            foreach (var order in orders)
            {
                _context.TbOrderDetails.RemoveRange(order.TbOrderDetails); // Xóa tất cả chi tiết đơn hàng
                _context.TbOrders.Remove(order); // Xóa đơn hàng
            }

            _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            return RedirectToAction("Index"); // Chuyển hướng về trang giỏ hàng
        }

        [HttpPost]
        public IActionResult PlaceOrder(TbOrder model)
        {
            if (ModelState.IsValid)
            {
                // Tạo mới một đối tượng TbOrder
                var order = new TbOrder
                {
                    CustomerName = model.CustomerName,
                    Phone = model.Phone,
                    Address = model.Address,
                    TotalAmount = model.TotalAmount,
                    Quantity = model.Quantity,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                };

                // Thêm đơn hàng vào DbContext
                _context.TbOrders.Add(order);
                _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                // Xóa giỏ hàng sau khi thanh toán
                ClearCart(); // Gọi phương thức ClearCart để xóa giỏ hàng

                // Chuyển hướng đến trang thành công hoặc trang nào đó
                return RedirectToAction("OrderSuccess"); // Đặt tên cho action thành công của bạn
            }

            // Nếu model không hợp lệ, trả lại view với model để hiển thị lỗi
            return View(model);
        }


        public IActionResult Checkout()
        {
            var cartItems = _context.TbOrders
               .Include(o => o.TbOrderDetails) // Giả sử rằng TbOrder có một mối quan hệ với TbOrderDetail
                   .ThenInclude(d => d.Product) // Giả sử TbOrderDetail có một mối quan hệ với TbProduct
               .ToList();
            return View(cartItems);
        }

        // Xử lý form thêm sản phẩm vào giỏ hàng

        [HttpPost]
        public async Task<IActionResult> Create(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                ModelState.AddModelError("", "Số lượng phải lớn hơn 0.");
                ViewBag.CartItemCount = await GetCartItemCountAsync();
                return View();
            }

            var existingOrderDetail = await _context.TbOrderDetails
                .Include(od => od.Order)
                .FirstOrDefaultAsync(od => od.ProductId == productId && od.Order.Status == null);

            if (existingOrderDetail != null)
            {
                existingOrderDetail.Quantity += quantity;

                var order = existingOrderDetail.Order;
                order.TotalAmount += existingOrderDetail.Price * quantity;

                _context.TbOrderDetails.Update(existingOrderDetail);
                _context.TbOrders.Update(order);
            }
            else
            {
                var product = await _context.TbProducts.FindAsync(productId);
                if (product == null)
                {
                    return NotFound();
                }

                var order = new TbOrder
                {
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    TbOrderDetails = new List<TbOrderDetail>
            {
                new TbOrderDetail
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price
                }
            },
                    TotalAmount = product.Price * quantity
                };

                await _context.TbOrders.AddAsync(order);
            }

            await _context.SaveChangesAsync();
            ViewBag.CartItemCount = await GetCartItemCountAsync();

            return RedirectToAction("Index");
        }

        private async Task<int> GetCartItemCountAsync()
        {
            return await _context.TbOrderDetails
                .Where(od => od.Order.Status == null) // Giả sử chúng ta chỉ muốn đếm các đơn hàng hoạt động
                .SumAsync(od => od.Quantity);
        }


        // Hiển thị form chỉnh sửa sản phẩm trong giỏ hàng
        public IActionResult Edit(int id)
        {
            var order = _context.TbOrders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // Xử lý form chỉnh sửa sản phẩm trong giỏ hàng
        [HttpPost]
        public IActionResult Edit(TbOrder order)
        {
            if (ModelState.IsValid)
            {
                _context.TbOrders.Update(order);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var orderDetail = _context.TbOrderDetails.Include(od => od.Order).FirstOrDefault(od => od.Id == id);

            if (orderDetail != null)
            {
                var order = orderDetail.Order; // Lấy đơn hàng tương ứng với chi tiết đơn hàng

                // Xóa chi tiết đơn hàng
                _context.TbOrderDetails.Remove(orderDetail);

                // Kiểm tra xem đơn hàng có còn chi tiết nào khác không
                if (!order.TbOrderDetails.Any()) // Nếu không còn chi tiết nào
                {
                    _context.TbOrders.Remove(order); // Xóa đơn hàng
                }

                _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateCart(Dictionary<int, int> quantities)
        {
            if (quantities == null || !quantities.Any())
            {
                return RedirectToAction("Index"); // Trở lại trang giỏ hàng nếu không có sản phẩm nào
            }

            // Lặp qua từng sản phẩm trong giỏ hàng và cập nhật số lượng
            foreach (var item in quantities)
            {
                var orderDetail = _context.TbOrderDetails
                    .Include(od => od.Order)
                    .ThenInclude(o => o.TbOrderDetails)
                    .FirstOrDefault(od => od.Id == item.Key); // Tìm orderDetail theo ID

                if (orderDetail != null)
                {
                    // Cập nhật số lượng
                    orderDetail.Quantity = item.Value;

                    // Tính toán lại TotalAmount cho đơn hàng
                    var order = orderDetail.Order;
                    decimal totalAmount = 0;
                    foreach (var detail in order.TbOrderDetails)
                    {
                        totalAmount += detail.Price * detail.Quantity;
                    }

                    // Cập nhật lại tổng số tiền
                    order.TotalAmount = totalAmount;

                    // Lưu lại thay đổi
                    _context.TbOrderDetails.Update(orderDetail);
                    _context.TbOrders.Update(order);
                }
            }

            _context.SaveChanges();  // Lưu vào cơ sở dữ liệu

            return RedirectToAction("Index"); // Quay lại trang giỏ hàng
        }


        public int GetCartItemCount()
        {
            // Tính tổng số lượng sản phẩm trong tất cả các chi tiết đơn hàng
            var cartItemCount = _context.TbOrderDetails.Sum(od => od.Quantity);
            return cartItemCount;
        }

    }
}