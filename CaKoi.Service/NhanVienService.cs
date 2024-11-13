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
        public NhanVien Loginn(string username, string password)
        {
            return _respository.GetEmployeeByUandP(username, password);
        }
    }
}
