namespace ktragiuaky.Models
{
    public class ChiTietDangKy
    {
        public int MaDk { get; set; }
        public string MaHp { get; set; }

        public DangKy DangKy { get; set; }
        public HocPhan HocPhan { get; set; }
    }
}
