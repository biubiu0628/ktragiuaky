using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ktragiuaky.Models;

public partial class NganhHoc
{
    [Key]
    public string MaNganh { get; set; } = null!;

    public string? TenNganh { get; set; }

    public virtual ICollection<SinhVien> SinhViens { get; set; } = new List<SinhVien>();
}
