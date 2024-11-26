﻿using System;
using System.Collections.Generic;

namespace e_commerce_AP.Models.EF;

public partial class TbProductImage
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string? Image { get; set; }

    public bool IsDefault { get; set; }

    public virtual TbProduct Product { get; set; } = null!;
}