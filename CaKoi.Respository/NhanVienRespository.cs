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
    public class NhanVienRespository : INhanVienRespository
    {
        public readonly CaKoiContext _context;
        public NhanVienRespository(CaKoiContext context)
        {
            _context = context;
        }

        public bool AddNhanVien(NhanVien model)
        {
            _context.NhanViens.Add(model);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> CapnhatNV(string idnv, NhanVien nv)
        {
            var nhanv = await _context.NhanViens.FindAsync(idnv);
            if (nhanv == null) return false;
            
            nhanv.TenTaiKhoan = nv.TenTaiKhoan;
            nhanv.MatKhau = nv.MatKhau;
            nhanv.Hoten = nv.Hoten;
            nhanv.Ngaysinh = nv.Ngaysinh;
            nhanv.Gioitinh = nv.Gioitinh;
            nhanv.Sdt = nv.Sdt;
            nhanv.Email = nv.Email;
            nhanv.ChucVu = nv.ChucVu;
            await _context.SaveChangesAsync(); // Lưu thay đổi
            return true;
        }

        public async Task<bool> DeleteNV(string id)
        {
            var nhanv = await _context.NhanViens.FindAsync(id);
            if (nhanv != null)
            {
                _context.NhanViens.Remove(nhanv);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public NhanVien GetEmployeeByUandP(string username, string password)
        {
            return _context.NhanViens.FirstOrDefault(user => user.TenTaiKhoan == username && user.MatKhau == password);
        }

        public async Task<NhanVien> GetNhanVienById(string id)
        {
            return await _context.NhanViens.FindAsync(id);
        }

        public async Task<List<NhanVien>> GetNhanViens()
        {
            return await _context.NhanViens.ToListAsync();
        }
    }
}
