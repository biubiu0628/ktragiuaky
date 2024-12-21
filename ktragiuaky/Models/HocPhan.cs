using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ktragiuaky.Models;

public partial class HocPhan
{
    [Key]
    public string MaHp { get; set; } = null!;

    public string TenHp { get; set; } = null!;

    public int? SoTinChi { get; set; }

    public virtual ICollection<ChiTietDangKy> ChiTietDangKies { get; set; } = new List<ChiTietDangKy>();

}
