using CaKoi.Respository.Entities;
using CaKoi.Respository.Interface;
using CaKoi.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CaKoi.Service
{
    public class KhachHangService : IKhachHangService
    {
        private readonly IKhachHangRespository _respository;
        public KhachHangService(IKhachHangRespository respository)
        {
            _respository = respository;
        }

        public bool AddKhachHang(KhachHang model)
        {
            _respository.AddKhachHang(model);
            return true;
        }
        public KhachHang GetKhachByID(int id)
        {
            return _respository.GetKhachByID(id);
        }

        public KhachHang GetKhachHangByTenTaiKhoan(string tenTaiKhoan)
        {
            return _respository.GetKhachHangByTenTaiKhoan(tenTaiKhoan);
        }

        public Task<List<KhachHang>> GetKhachHangs()
        {
            return _respository.GetKhachHangs();
        }

        public KhachHang Login(string username, string password)
        {
            return _respository.GetUserByUsernameAndPassword(username, password);
        }
    }
}
