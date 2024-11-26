using System;
using System.Collections.Generic;

namespace e_commerce_AP.Models.EF;

public partial class TbSystemSetting
{
    public string SettingKey { get; set; } = null!;

    public string? SettingValue { get; set; }

    public string? SettingDescription { get; set; }
}