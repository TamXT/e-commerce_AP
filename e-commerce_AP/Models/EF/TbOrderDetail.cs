using e_commerce_AP.Models.EF;
using System;
using System.Collections.Generic;

namespace e_commerce_AP.Models.EF;

public partial class TbOrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public virtual TbOrder? Order { get; set; }

    public virtual TbProduct? Product { get; set; }
}