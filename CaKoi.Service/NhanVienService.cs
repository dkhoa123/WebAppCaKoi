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
    public class NhanVienService : INhanVienService
    {
        private readonly INhanVienRespository _respository;
        public NhanVienService(INhanVienRespository respository)
        {
            _respository = respository;
        }

        public bool AddNhanVien(NhanVien model)
        {
            _respository.AddNhanVien(model);
            return true;
        }

        public async Task<bool> CapnhatNV(string idnv, NhanVien nv)
        {
            return await _respository.CapnhatNV(idnv, nv);
        }
        public async Task<bool> DeleteNV(string id)
        {
            return await _respository.DeleteNV(id);
        }

        public async Task<NhanVien> GetNhanVienById(string id)
        {
            return await _respository.GetNhanVienById(id);
        }

        public Task<List<NhanVien>> GetNhanViens()
        {
            return _respository.GetNhanViens();
        }

        public NhanVien Loginn(string username, string password)
        {
            return _respository.GetEmployeeByUandP(username, password);
        }
    }
}
