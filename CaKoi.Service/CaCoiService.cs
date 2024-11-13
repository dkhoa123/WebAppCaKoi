using CaKoi.Respository.Entities;
using CaKoi.Respository.Interface;
using CaKoi.Service.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaKoi.Service
{
    public class CaCoiService : ICaCoiService
    {
        public readonly ICaCoiRespository _respository;
        public CaCoiService(ICaCoiRespository respository)
        {
            _respository = respository;
        }
        public CaCoi GetCaKoiByID(int id)
        {
            return _respository.GetItemByCaKoiId(id);
        }
       
        public Task<List<CaCoi>> GetCaCois()
        {
            return _respository.GetCaCois();
        }
    }
}
