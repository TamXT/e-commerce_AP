namespace e_commerce_AP.Models.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public IEnumerable<e_commerce_AP.Models.EF.TbOrderDetail> OrderDetails { get; set; }
    }

}
