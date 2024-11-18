using CaKoi.Respository.Entities;
using CaKoi.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WebCaKoi.Controllers
{

    public class AdminController : Controller
    {
        private readonly IDonHangService _dhservice;
        private readonly INhanVienService _nvservice;
        private readonly IKhachHangService _khservice;
        private readonly ICaCoiService _caService;
        public AdminController(IDonHangService dhservice, INhanVienService nvservice, IKhachHangService khservice, ICaCoiService caService)
        {
            _dhservice = dhservice;
            _nvservice = nvservice;
            _khservice = khservice;
            _caService = caService;
        }
        // GET: AdminController
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<ActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("Idad");
            if (userId == null)
            {
                return RedirectToAction("LoginAD");
            }
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
        [Authorize(AuthenticationSchemes = "AdminCookie")]
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
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _dhservice.DeleteDonHang(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult LoginAD()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAD(string TenTaiKhoan, string MatKhau, string idad = "ad")
        {

            // Tìm khách hàng từ cơ sở dữ liệu theo tên tài khoản và mật khẩu
            if (TenTaiKhoan == "admin" & MatKhau == "admin123")
            {
                // Đăng nhập thành công, tạo claims cho người dùng
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, TenTaiKhoan),
            new Claim("Idad", idad.ToString())
        };

                var identity = new ClaimsIdentity(claims, "AdminCookie");
                var principal = new ClaimsPrincipal(identity);

                // Sử dụng Cookie Authentication để lưu đăng nhập
                await HttpContext.SignInAsync("AdminCookie", principal);

                // Nếu cần thiết, lưu thêm thông tin vào session
                HttpContext.Session.SetString("Admin", TenTaiKhoan);
                HttpContext.Session.SetString("Idad", idad);

                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Error = "Tài khoản hoặc mật khẩu không đúng!";
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            // Thực hiện đăng xuất
            await HttpContext.SignOutAsync("AdminCookie");

            // Chuyển hướng đến trang chủ hoặc trang đăng nhập
            return RedirectToAction("LoginAD","Admin");
        }


        //PHẦN CỦA NHÂN VIÊN
        [Authorize(AuthenticationSchemes = "AdminCookie")]
        public async Task<IActionResult> ListNhanVien()
        {
            var nv = await _nvservice.GetNhanViens();
            return View(nv);
        }
        public IActionResult CreateNV()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNV(NhanVien nv)
        {
            if(nv != null)
            {
                _nvservice.AddNhanVien(nv);
                return RedirectToAction("ListNhanVien");
            }
            return View();
        }
        public async Task<IActionResult> EditNV(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(); // Kiểm tra nếu id null hoặc rỗng
            }

            var nv = await _nvservice.GetNhanVienById(id); // Hàm lấy nhân viên theo id
            if (nv == null)
            {
                return NotFound(); // Nếu không tìm thấy nhân viên
            }

            return View(nv); // Hiển thị thông tin lên form
        }
        [HttpPost]
        public async Task<IActionResult> EditNV(string id, NhanVien nv)
        {
            if (ModelState.IsValid)
            {
                nv.Idnv = id;
                var result = await _nvservice.CapnhatNV(id, nv);
                if (result)
                {
                    // Redirect hoặc hiển thị thông báo thành công
                    return RedirectToAction("ListNhanVien"); // Hoặc trang khác bạn muốn
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật trạng thái không thành công.");
                }
            }

            return View(nv); // Trả về view nếu có lỗi
        }
        public async Task<IActionResult> DeleteNhanVien(string id)
        {

            var result = await _nvservice.DeleteNV(id);
            if (result)
            {
                return RedirectToAction("ListNhanVien");
            }
            return View();
        }

        public async Task<IActionResult> ListKhachHang()
        {
            var kh = await _khservice.GetKhachHangs();
            return View(kh);
        }
       
        public IActionResult EditKH(int id)
        {
            var kh =  _khservice.GetKhachByID(id);
            if (kh == null)
            {
                return NotFound();
            }

            return View(kh);
        }
        [HttpPost]
        public async Task<IActionResult> EditKH(int id, KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                kh.Idkh = id;
                var result = await _khservice.CapnhatKH(id, kh);
                if (result)
                {
                    // Redirect hoặc hiển thị thông báo thành công
                    return RedirectToAction("ListKhachHang"); // Hoặc trang khác bạn muốn
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật trạng thái không thành công.");
                }
            }

            return View(kh); // Trả về view nếu có lỗi
        }
        public async Task<IActionResult> DeleteKhachHang(int id)
        {

            var result = await _khservice.DeleteKH(id);
            if (result)
            {
                return RedirectToAction("ListKhachHang");
            }
            return View();
        }

        // DANH CHO CA KOI 
        public async Task<IActionResult> ListCaKoi()
        {
            var ca = await _caService.GetCaCois();
            return View(ca);
        }
        public IActionResult CreateCa()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCa(CaCoi ca)
        {
            if (ca != null)
            {
                _caService.AddCa(ca);
                return RedirectToAction("ListCaKoi");
            }
            return View();
        }
        public IActionResult EditCa(int id)
        {
            var ca = _caService.GetCaKoiByID(id);
            if (ca == null)
            {
                return NotFound();
            }

            return View(ca);
        }
        [HttpPost]
        public async Task<IActionResult> EditCa(int id, CaCoi ca)
        {
            if (ModelState.IsValid)
            {
                ca.IdcaKoi = id;
                var result = await _caService.CapNhatCa(id, ca);
                if (result)
                {
                    // Redirect hoặc hiển thị thông báo thành công
                    return RedirectToAction("ListCaKoi"); // Hoặc trang khác bạn muốn
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật trạng thái không thành công.");
                }
            }

            return View(ca); // Trả về view nếu có lỗi
        }
        public async Task<IActionResult> DeleteCa(int id)
        {

            var result = await _caService.DeleteCa(id);
            if (result)
            {
                return RedirectToAction("ListCaKoi");
            }
            return View();
        }
        public IActionResult SomeAction()
        {
            // Kiểm tra xem trong claims của người dùng có claim nào với tên "IdKh" hoặc "Idnv"
            var isCustomer = User.Claims.FirstOrDefault(c => c.Type == "IdKh") != null;
            var isEmployee = User.Claims.FirstOrDefault(c => c.Type == "Idnv") != null;
            var isAdmin = User.Claims.FirstOrDefault(c => c.Type == "Idad") != null;

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
            else if (isAdmin)
            {
                // Xử lý cho nhân viên
                return View("Index", "Admin"); // Ví dụ trả về một view riêng cho nhân viên
            }
            else
            {
                // Nếu không phải khách hàng hay nhân viên
                return Unauthorized(); // Hoặc chuyển hướng đến trang đăng nhập hoặc lỗi
            }
        }

    }
}
