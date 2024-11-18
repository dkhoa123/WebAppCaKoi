using CaKoi.Respository.Entities;
using CaKoi.Service;
using CaKoi.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebCaKoi.Controllers
{
   
    public class NhanVienController : Controller
    {
        private readonly IDonHangService _service;
        private readonly INhanVienService _nvService;
        public NhanVienController(IDonHangService service, INhanVienService nvService)
        {
            _service = service;
            _nvService = nvService;
        }
        [Authorize(AuthenticationSchemes = "EmployeeCookie")]
        public async Task<IActionResult> Index()
        {
            var donhang = await _service.GetDonHangs();
            return View(donhang);
        }
        public IActionResult LoginNhanVien()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginNhanVien(string username, string pass)
        {
            var nhanvien = _nvService.Loginn(username, pass);
            if (nhanvien != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, nhanvien.TenTaiKhoan),
            new Claim("Idnv", nhanvien.Idnv.ToString())
        };

                var identity = new ClaimsIdentity(claims, "EmployeeCookie");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("EmployeeCookie", principal);
                HttpContext.Session.SetString("NhanVien", nhanvien.TenTaiKhoan);
                HttpContext.Session.SetString("Idnv", nhanvien.Idnv);

                return RedirectToAction("Index", "NhanVien");
            }

            ViewBag.Error = "Tài khoản hoặc mật khẩu không đúng!";
            return View();
        }
        public async Task<IActionResult> LogoutNhanVien()
        {
            HttpContext.Session.Clear();
            // Thực hiện đăng xuất
            await HttpContext.SignOutAsync("EmployeeCookie");

            // Chuyển hướng đến trang chủ hoặc trang đăng nhập
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var donchitiet = await _service.GetDonHangChiTiets(id);
            if (donchitiet == null || donchitiet.Count == 0)
            {
                return NotFound();
            }
            return View(donchitiet);
        }

        public async Task<IActionResult> Edit(int id, DateOnly ngaygiao, string trangthai)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.EditDonHang(id,ngaygiao,trangthai);
                if (result)
                {
                    // Redirect hoặc hiển thị thông báo thành công
                    return RedirectToAction("Index"); // Hoặc trang khác bạn muốn
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật trạng thái không thành công.");
                }
            }

            return View(); // Trả về view nếu có lỗi
        }

        public async Task<IActionResult> Delete(int id)
        {

            var result = await _service.DeleteDonHang(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
