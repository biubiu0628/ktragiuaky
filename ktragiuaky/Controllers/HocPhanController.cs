using ktragiuaky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ktragiuaky.Controllers
{
    public class HocPhanController : Controller
    {
        private readonly QlsvContext _context;

        public HocPhanController(QlsvContext context)
        {
            _context = context;
        }

        // GET: SinhViens
        public async Task<IActionResult> Index()
        {
            var hocPhans = await _context.HocPhan.ToListAsync();
            return View(hocPhans);
        }

        [HttpPost]
        public async Task<IActionResult> Register(List<string> selectedMaHps)
        {
            var maSv = HttpContext.Session.GetString("MaSv");
            if (string.IsNullOrEmpty(maSv))
            {
                return RedirectToAction("Login", "Account");
            }

            var existingRegistration = await _context.DangKy
                .FirstOrDefaultAsync(d => d.MaSv == maSv);

            if (existingRegistration == null)
            {
                existingRegistration = new DangKy
                {
                    MaSv = maSv,
                    NgayDk = DateOnly.FromDateTime(DateTime.Now),
                    ChiTietDangKies = new List<ChiTietDangKy>()
                };
                _context.DangKy.Add(existingRegistration);
                await _context.SaveChangesAsync();
            }

            foreach (var maHp in selectedMaHps)
            {
                var hocPhan = await _context.HocPhan.FirstOrDefaultAsync(h => h.MaHp == maHp);
                if (hocPhan != null)
                {
                    var existingChiTiet = await _context.ChiTietDangKy
                        .FirstOrDefaultAsync(cd => cd.MaDk == existingRegistration.MaDk && cd.MaHp == maHp);

                    if (existingChiTiet == null)
                    {
                        existingRegistration.ChiTietDangKies.Add(new ChiTietDangKy
                        {
                            MaDk = existingRegistration.MaDk,
                            MaHp = maHp
                        });
                    }
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("GioHang", "HocPhan");
        }

        public async Task<IActionResult> GioHang()
        {
            var maSv = HttpContext.Session.GetString("MaSv");
            if (string.IsNullOrEmpty(maSv))
            {
                return RedirectToAction("Login", "Account");
            }

            var dangKy = await _context.DangKy
                .Include(d => d.ChiTietDangKies)
                .ThenInclude(cd => cd.HocPhan)
                .FirstOrDefaultAsync(d => d.MaSv == maSv);

            if (dangKy == null)
            {
                return View(new List<HocPhan>());
            }

            var hocPhans = dangKy.ChiTietDangKies.Select(cd => cd.HocPhan).ToList();

            return View(hocPhans);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string maHp)
        {
            var maSv = HttpContext.Session.GetString("MaSv");
            if (string.IsNullOrEmpty(maSv))
            {
                return RedirectToAction("Login", "Account");
            }

            var dangKy = await _context.DangKy
                .Include(d => d.ChiTietDangKies)
                .FirstOrDefaultAsync(d => d.MaSv == maSv);

            if (dangKy != null)
            {
                var chiTietDangKy = dangKy.ChiTietDangKies
                    .FirstOrDefault(cd => cd.MaHp == maHp);

                if (chiTietDangKy != null)
                {
                    dangKy.ChiTietDangKies.Remove(chiTietDangKy);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("GioHang");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAll()
        {
            var maSv = HttpContext.Session.GetString("MaSv");
            if (string.IsNullOrEmpty(maSv))
            {
                return RedirectToAction("Login", "Account");
            }

            var dangKy = await _context.DangKy
                .Include(d => d.ChiTietDangKies)
                .FirstOrDefaultAsync(d => d.MaSv == maSv);

            if (dangKy != null)
            {
                dangKy.ChiTietDangKies.Clear();
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("GioHang");
        }

        [HttpPost]
        public async Task<IActionResult> Save()
        {
            var maSv = HttpContext.Session.GetString("MaSv");
            if (string.IsNullOrEmpty(maSv))
            {
                return RedirectToAction("Login", "Account");
            }

            // Lấy giỏ hàng (danh sách học phần đã đăng ký)
            var dangKy = await _context.DangKy
                .Include(d => d.ChiTietDangKies)
                .FirstOrDefaultAsync(d => d.MaSv == maSv);

            if (dangKy == null || dangKy.ChiTietDangKies.Count == 0)
            {
                // Nếu không có học phần nào trong giỏ, trả về thông báo
                ViewBag.Message = "Bạn chưa chọn học phần nào để đăng ký.";
                return RedirectToAction("GioHang");  // Quay lại trang giỏ hàng
            }

            // Lưu các học phần vào bảng ChiTietDangKy (giả sử bảng này đã có thông tin về học phần và sinh viên)
            foreach (var chiTiet in dangKy.ChiTietDangKies)
            {
                // Có thể thêm logic để kiểm tra trước khi lưu, ví dụ: kiểm tra xem sinh viên đã đăng ký học phần này chưa
                var existingRegistration = await _context.ChiTietDangKy
                    .FirstOrDefaultAsync(cd => cd.MaDk == dangKy.MaDk && cd.MaHp == chiTiet.MaHp);

                if (existingRegistration == null)
                {
                    // Nếu chưa có, thêm mới thông tin đăng ký học phần
                    _context.ChiTietDangKy.Add(chiTiet);
                }
            }

            await _context.SaveChangesAsync();
            return View("Save");
        }
    }
}
