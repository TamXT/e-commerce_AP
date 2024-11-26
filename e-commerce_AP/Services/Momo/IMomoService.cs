using e_commerce_AP.Models;
using e_commerce_AP.Models.Momo;


namespace e_commerce_AP.Services.Momo
{
    public interface IMomoService
    {
        Task<MonoCreatePaymentResponseModel> CreatePaymentMomo(OrderInfo model);

        MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
    }
}
