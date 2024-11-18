using CaKoi.Respository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaKoi.Service.Interface
{
    public interface ICaCoiService
    {
        Task<List<CaCoi>> GetCaCois();
        CaCoi GetCaKoiByID(int id);
        Boolean AddCa(CaCoi caCoi);
        Task<bool> CapNhatCa(int id, CaCoi ca);
        Task<bool> DeleteCa(int id);
    }
}
