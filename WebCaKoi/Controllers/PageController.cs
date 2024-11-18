using CaKoi.Respository.Entities;
using CaKoi.Service;
using CaKoi.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCaKoi.Models;

namespace WebCaKoi.Controllers
{

    public class PageController : Controller
    {

        private readonly IDonHangService _service;
        private readonly ICartService _cartService;
        private readonly ICaCoiService _caCoiService;
        private readonly IKhachHangService _khservice;
        public PageController(IDonHangService service, ICartService cartService, ICaCoiService caCoiService, IKhachHangService khservice)
        {
            _service = service;
            _cartService = cartService;
            _caCoiService = caCoiService;
            _khservice = khservice;
        }
        public async Task<IActionResult> DonHang()
        {
            var userId = HttpContext.Session.GetInt32("IdKh");
            if (userId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var lsdonhang = await _service.GetDonHangs();
            return View(lsdonhang);
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
        public async Task<IActionResult> ShopDetail()
        {
            var ca = await _caCoiService.GetCaCois();
            return View(ca);
        }
        public async Task<IActionResult> ShopCart()
        {
            var userId = HttpContext.Session.GetInt32("IdKh");
            if (userId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            
            var ca = await _service.GetDonHangChiTiets(userId.Value);
            var Total = _cartService.GetTotal(userId.Value);
            ViewBag.Total = Total;
            return View(ca);
        }
        public IActionResult AddToCart(int UserID, int id)
        {
            // Kiểm tra thông tin người dùng
            var idkh = _khservice.GetKhachByID(UserID);
            if (idkh == null)
            {
                // Log thông báo hoặc chuyển hướng đến trang đăng nhập
                return RedirectToAction("Login", "Home");
            }
           _cartService.AddToCart(UserID, id, 1);
            return RedirectToAction("ShopCart"); // hoặc trang khác bạn muốn
        }
        public IActionResult DeleteItem(int id)
        {
            _cartService.Deletecart(id);
            return RedirectToAction("ShopCart");
        }
        public IActionResult TrangTrai()
        {
            return View();
        }
    }
}
