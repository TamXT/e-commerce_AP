using Microsoft.AspNetCore.Mvc;

namespace e_commerce_AP.Areas.Admin.Controllers
{
    [Area("Admin")] // Correct attribute for areas
    public class ThemBaiVietController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
