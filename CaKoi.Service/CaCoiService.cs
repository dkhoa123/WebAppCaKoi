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

        public bool AddCa(CaCoi caCoi)
        {
            return _respository.AddCa(caCoi);
        }

        public async Task<bool> CapNhatCa(int id, CaCoi ca)
        {
            return await _respository.CapNhatCa(id, ca);
        }

        public async Task<bool> DeleteCa(int id)
        {
            return await _respository.DeleteCa(id);
        }
    }
}
