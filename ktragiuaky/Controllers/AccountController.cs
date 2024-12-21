using ktragiuaky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ktragiuaky.Controllers
{
    public class AccountController : Controller
    {
        private readonly QlsvContext _context;

        public AccountController(QlsvContext context)
        {
            _context = context;
        }

        // GET: Trang đăng nhập
        public IActionResult Login()
        {
            return View();
        }

        // POST: Xử lý đăng nhập
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string maSv)
        {
            if (string.IsNullOrEmpty(maSv))
            {
                ModelState.AddModelError("", "Mã số sinh viên không được để trống.");
                return View();
            }

            var sinhVien = await _context.SinhVien.FirstOrDefaultAsync(sv => sv.MaSv == maSv);

            if (sinhVien == null)
            {
                ModelState.AddModelError("", "Không tìm thấy sinh viên với mã số này.");
                return View();
            }

            HttpContext.Session.SetString("MaSv", sinhVien.MaSv);
            HttpContext.Session.SetString("HoTen", sinhVien.HoTen);

            return RedirectToAction("Index", "SinhVien");
        }

        // GET: Đăng xuất
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }

}
