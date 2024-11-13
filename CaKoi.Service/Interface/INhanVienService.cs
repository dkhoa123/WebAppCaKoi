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
    }
}

