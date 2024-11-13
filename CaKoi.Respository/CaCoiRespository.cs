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
    public class CaCoiRespository : ICaCoiRespository
    {
        public readonly CaKoiContext _db;
        public CaCoiRespository(CaKoiContext db)
        {
            _db = db;
        }
        public void DeleteCa(int id)
        {
            var ca = _db.CaCois.FirstOrDefault(i => i.IdcaKoi == id);
            if (ca != null)
            {
                _db.CaCois.Remove(ca);
                _db.SaveChanges();
            }
        }

        public async Task<List<CaCoi>> GetCaCois()
        {
            return await _db.CaCois.ToListAsync();
        }
        
        public CaCoi GetItemByCaKoiId(int id)
        {
            return _db.CaCois.Where(ca => ca.IdcaKoi == id).FirstOrDefault();
        }
    }
}
