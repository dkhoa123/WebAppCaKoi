using CaKoi.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebCaKoi.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDonHangService _dhservice;
        public AdminController(IDonHangService dhservice)
        {
            _dhservice = dhservice;        
        }
        // GET: AdminController
        public async Task<ActionResult> Index()
        {
            var donhang = await _dhservice.GetDonHangs();
            return View(donhang);
        }

        // GET: AdminController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var donchitiet = await _dhservice.GetDonHangChiTiets(id);
            if (donchitiet == null || donchitiet.Count == 0)
            {
                return NotFound();
            }
            return View(donchitiet);
        }

        public async Task<IActionResult> Edit(int id, string choduyet)
        {
            if (ModelState.IsValid)
            {
                var result = await _dhservice.AdminEditDonHang(id, choduyet);
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

        // GET: AdminController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _dhservice.DeleteDonHang(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult SomeAction()
        {
            // Kiểm tra xem trong claims của người dùng có claim nào với tên "IdKh" hoặc "Idnv"
            var isCustomer = User.Claims.FirstOrDefault(c => c.Type == "IdKh") != null;
            var isEmployee = User.Claims.FirstOrDefault(c => c.Type == "Idnv") != null;

            if (isCustomer)
            {
                // Xử lý cho khách hàng
                return View("Index", "Home"); // Ví dụ trả về một view riêng cho khách hàng
            }
            else if (isEmployee)
            {
                // Xử lý cho nhân viên
                return View("Index", "NhanVien"); // Ví dụ trả về một view riêng cho nhân viên
            }
            else
            {
                // Nếu không phải khách hàng hay nhân viên
                return Unauthorized(); // Hoặc chuyển hướng đến trang đăng nhập hoặc lỗi
            }
        }
       
    }
}
