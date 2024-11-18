using CaKoi.Respository.Entities;
using CaKoi.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebCaKoi.Models;
using Microsoft.AspNetCore.Authorization;


namespace WebCaKoi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly IKhachHangService _khachservice;
        public readonly ICaCoiService _caCoiService;
        public HomeController(ILogger<HomeController> logger, IKhachHangService service, ICaCoiService caCoiService)
        {
            _caCoiService = caCoiService;
            _khachservice = service;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var ca = await _caCoiService.GetCaCois();
            return View(ca);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string TenTaiKhoan, string MatKhau)
        {

            // Tìm khách hàng từ cơ sở dữ liệu theo tên tài khoản và mật khẩu
            var user = _khachservice.Login(TenTaiKhoan, MatKhau);
            if (user != null)
            {
                // Đăng nhập thành công, tạo claims cho người dùng
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.TenTaiKhoan),
            new Claim("IdKh", user.Idkh.ToString())
        };

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);

                // Sử dụng Cookie Authentication để lưu đăng nhập
                await HttpContext.SignInAsync("CookieAuth", principal);

                // Nếu cần thiết, lưu thêm thông tin vào session
                HttpContext.Session.SetString("User", user.TenTaiKhoan);
                HttpContext.Session.SetInt32("IdKh", user.Idkh);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Tài khoản hoặc mật khẩu không đúng!";
            return View();
        }
        

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            // Thực hiện đăng xuất
            await HttpContext.SignOutAsync("CookieAuth");

            // Chuyển hướng đến trang chủ hoặc trang đăng nhập
            return RedirectToAction("Index", "Home");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(KhachHang model)
        {

            if (model != null)
            {
                _khachservice.AddKhachHang(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Contact()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
