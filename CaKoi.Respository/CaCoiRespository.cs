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

        public bool AddCa(CaCoi ca)
        {
            _db.Add(ca);
            _db.SaveChanges();
            return true;
        }

        public async Task<bool> CapNhatCa(int id, CaCoi ca)
        {
            var cacoi = await _db.CaCois.FindAsync(id);
            if(cacoi == null) return false;
            cacoi.TenLoai = ca.TenLoai;
            cacoi.Gia = ca.Gia;
            cacoi.Tuoi = ca.Tuoi;
            cacoi.HinhAnh = ca.HinhAnh;
            cacoi.SoLuong = ca.SoLuong;
            cacoi.Idloai = ca.Idloai;
            _db.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteCa(int id)
        {
            var ca = await _db.CaCois.FindAsync(id);
            if (ca != null)
            {
                _db.CaCois.Remove(ca);
                _db.SaveChanges();
            }
            return true;
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
