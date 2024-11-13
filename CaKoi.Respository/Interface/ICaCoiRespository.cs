using CaKoi.Respository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaKoi.Respository.Interface
{
    public interface ICaCoiRespository
    {
        CaCoi GetItemByCaKoiId(int id);
        Task<List<CaCoi>> GetCaCois();
        void DeleteCa(int id);
    }
}
