using CaKoi.Respository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaKoi.Respository.Interface
{
    public interface INhanVienRespository
    {
        NhanVien GetEmployeeByUandP(string username, string password);

    }
}
