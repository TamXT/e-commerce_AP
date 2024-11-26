using Microsoft.AspNetCore.Mvc;

namespace e_commerce_AP.Controllers
{
    public class LoginController : Controller
    {  
        public IActionResult Index()
        {
            return View();
        }
    }
}
