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
        public NhanVien GetEmployeeByUandP(string username, string password)
        {
            return _context.NhanViens.FirstOrDefault(user => user.TenTaiKhoan == username && user.MatKhau == password);
        }
    }
}
