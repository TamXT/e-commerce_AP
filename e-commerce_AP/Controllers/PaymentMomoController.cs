using e_commerce_AP.Models;
using e_commerce_AP.Services.Momo;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_AP.Controllers
{
   
    public class PaymentMomoController : Controller
    {
        private IMomoService _momoService;

        public PaymentMomoController(IMomoService momoService)
        {
            _momoService = momoService;

        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentMomo(OrderInfo model)
        {
            var response = await _momoService.CreatePaymentMomo(model);
            return Redirect(response.PayUrl);
        }

        [HttpGet]
        public IActionResult PaymentCallBack()
        {
            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            return View(response);
        }

    }
}
