using CaKoi.Respository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaKoi.Service.Interface
{
    public interface IKhachHangService
    {
        Task<List<KhachHang>> GetKhachHangs();
        Boolean AddKhachHang(KhachHang model);
        KhachHang GetKhachHangByTenTaiKhoan(string tenTaiKhoan);
        KhachHang Login(string username, string password);
        KhachHang GetKhachByID(int id);
    }
}
