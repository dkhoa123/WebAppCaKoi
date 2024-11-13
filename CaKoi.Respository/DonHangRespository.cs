using CaKoi.Respository.Entities;
using CaKoi.Respository.Interface;
using Microsoft.EntityFrameworkCore;
namespace CaKoi.Respository
{
    public class DonHangRespository : IDonHangRespository
    {
        public readonly CaKoiContext _db;
        public DonHangRespository(CaKoiContext db)
        {
            _db = db;

        }

        public bool AddDonHang(DonHang model)
        {
            _db.Add(model);
            _db.SaveChanges();
            return true;
        }

        public async Task<bool> AdminEditDonHang(int id, string choduyet)
        {
            var donHang = await _db.DonHangs.FindAsync(id);
            if (donHang.IddonHang == 0) return false;

            donHang.ChoDuyet = choduyet; // Cập nhật chờ duyệt
            await _db.SaveChangesAsync(); // Lưu thay đổi
            return true;
        }

        public async Task<bool> DeleteDonHang(int id)
        {
            var donhang = await _db.DonHangs.FindAsync(id);
            if (donhang != null) {
                _db.DonHangs.Remove(donhang);
               await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditDonHang(int id, DateOnly ngaygiao, string trangthai)
        {
            var donHang = await _db.DonHangs.FindAsync(id);
            if (donHang.IddonHang == 0) return false;

            donHang.NgayGiao = ngaygiao;
            donHang.TrangThai = trangthai; // Cập nhật trạng thái
            await _db.SaveChangesAsync(); // Lưu thay đổi
            return true;
        }
        public async Task<List<DonHangChiTiet>> GetDonHangChiTiets(int id)
        {
            
            return await _db.DonHangChiTiets.Where(dhct => dhct.Idkh == id).ToListAsync();
        }

        public async Task<List<DonHangChiTiet>> GetDonHangChiTiets()
        {
            return await _db.DonHangChiTiets.ToListAsync();
        }

        public async Task<List<DonHang>> GetDonHangs()
        {
            return await _db.DonHangs.ToListAsync();
        }
    }
}
