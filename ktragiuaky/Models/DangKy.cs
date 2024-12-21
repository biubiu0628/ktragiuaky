using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ktragiuaky.Models;

public partial class DangKy
{
    [Key]
    public int MaDk { get; set; }

    public DateOnly? NgayDk { get; set; }

    public string? MaSv { get; set; }

    public virtual SinhVien? MaSvNavigation { get; set; }

    public virtual ICollection<ChiTietDangKy> ChiTietDangKies { get; set; } = new List<ChiTietDangKy>();
}
