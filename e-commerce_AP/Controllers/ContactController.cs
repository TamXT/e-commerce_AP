using Microsoft.AspNetCore.Mvc;

namespace e_commerce_AP.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
