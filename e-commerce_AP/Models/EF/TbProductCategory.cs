using e_commerce_AP.Models.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace e_commerce_AP.Models.EF;

public partial class TbProductCategory
{
    [Key]

    public int Id { get; set; }

    [Required, MinLength(4, ErrorMessage = "Yêu cầu nhập Tên danh mục")]

    public string Title { get; set; } = null!;

    [Required, MinLength(4, ErrorMessage ="Yêu cầu nhập Mô tả danh mục")]
    public string? Description { get; set; }

    public string? Icon { get; set; }

    public string? SeoTitle { get; set; }

    public string? SeoDescription { get; set; }

    public string? SeoKeywords { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string? Modifiedby { get; set; }

    public string Alias { get; set; } = null!;

    public virtual ICollection<TbProduct> TbProducts { get; set; } = new List<TbProduct>();
}