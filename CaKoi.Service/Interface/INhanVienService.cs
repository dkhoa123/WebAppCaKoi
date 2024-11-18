using CaKoi.Respository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaKoi.Service.Interface
{
    public interface INhanVienService
    {
        NhanVien Loginn(string username, string password);
        Task<List<NhanVien>> GetNhanViens();
        Boolean AddNhanVien(NhanVien model);
        Task<bool> CapnhatNV(string idnv, NhanVien nv);
        Task<bool> DeleteNV(string id);
        Task<NhanVien> GetNhanVienById(string id);

    }
}

