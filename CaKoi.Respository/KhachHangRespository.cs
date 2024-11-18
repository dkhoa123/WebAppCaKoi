using CaKoi.Respository.Entities;
using CaKoi.Respository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaKoi.Respository
{
    public class KhachHangRespository : IKhachHangRespository
    {
        public readonly CaKoiContext _context;
        public KhachHangRespository(CaKoiContext context) {
            _context = context;
        }

        public bool AddKhachHang(KhachHang model)
        {
            _context.KhachHangs.Add(model);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> CapnhatKH(int idkh, KhachHang kh)
        {
            var khach = await _context.KhachHangs.FindAsync(idkh);
            if (khach == null) return false;

            khach.TenTaiKhoan = kh.TenTaiKhoan;
            khach.MatKhau = kh.MatKhau;
            khach.Hoten = kh.Hoten;
            khach.Ngaysinh = kh.Ngaysinh;
            khach.Gioitinh = kh.Gioitinh;
            khach.Sdt = kh.Sdt;
            khach.Email = kh.Email;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteKH(int id)
        {
            var kh = await _context.KhachHangs.FindAsync(id);
            if(kh != null)
            {
                _context.Remove(kh);
                _context.SaveChanges();
            }
            return true;
        }

        public KhachHang GetKhachByID(int id)
        {
            return _context.KhachHangs.FirstOrDefault(x => x.Idkh == id);
        }
        public KhachHang GetKhachHangByTenTaiKhoan(string tenTaiKhoan)
        {
            return _context.KhachHangs.FirstOrDefault(kh => kh.TenTaiKhoan == tenTaiKhoan);
        }

        public async Task<List<KhachHang>> GetKhachHangs()
        {
            return await _context.KhachHangs.ToListAsync();
        }

        public KhachHang GetUserByUsernameAndPassword(string username, string password)
        {
            return _context.KhachHangs.FirstOrDefault(user => user.TenTaiKhoan == username && user.MatKhau == password);
        }
    }
}
