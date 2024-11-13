using CaKoi.Respository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaKoi.Service.Interface
{
    public interface IDonHangService
    {
        Boolean AddDonHang(DonHang model);
        Task<List<DonHang>> GetDonHangs();
        Task<bool> EditDonHang(int id, DateOnly ngaygiao, string trangthai);
        Task<bool> AdminEditDonHang(int id, string choduyet);
        Task<bool> DeleteDonHang(int id);
        Task<List<DonHangChiTiet>> GetDonHangChiTiets(int id);
        Task<List<DonHangChiTiet>> GetDonHangChiTiets();
        
    }
}
