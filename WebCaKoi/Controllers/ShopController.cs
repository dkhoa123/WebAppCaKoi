
using CaKoi.Respository.Entities;
using CaKoi.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCaKoi.Controllers
{

    public class ShopController : Controller
    {

        private readonly ICaCoiService _service;
        private readonly IDonHangService _DHservice;
        public ShopController(ICaCoiService service, IDonHangService DHservice)
        {
            _service = service;
            _DHservice = DHservice;
        }

        public async Task<IActionResult> Index()
        {
            var ca = await _service.GetCaCois();
            return View(ca);
        }
        public IActionResult DonHang()
        {
            var UserID = HttpContext.Session.GetInt32("IdKh");
            var model = new DonHang
            {
                Idkh = UserID,
                TrangThai = "chua",
                ChoDuyet = "chua"
            }; // Gán vào ViewModel
            return View(model);
        }
        [HttpPost]
        public IActionResult DonHang(DonHang model)
        {
            _DHservice.AddDonHang(model);
            return RedirectToAction("DonHang", "Page");
        }
    }
}
