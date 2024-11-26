using System;
using System.Collections.Generic;

namespace e_commerce_AP.Models.EF;

public partial class TbSubscribe
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public DateTime? CreatedDate { get; set; } = DateTime.Now;
}