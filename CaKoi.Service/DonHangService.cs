using CaKoi.Respository;
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
    public class DonHangService : IDonHangService
    {
        public readonly IDonHangRespository _respository;
        public DonHangService(IDonHangRespository respository)
        {
            _respository = respository;
        }
        
        public bool AddDonHang(DonHang model)
        {
            return _respository.AddDonHang(model);
        }

        public async Task<bool> AdminEditDonHang(int id, string choduyet)
        {
            return await _respository.AdminEditDonHang(id, choduyet);
        }

        public async Task<bool> DeleteDonHang(int id)
        {
            return await _respository.DeleteDonHang(id);
        }

        public async Task<bool> EditDonHang(int id, DateOnly ngaygiao, string trangthai)
        {
            return await _respository.EditDonHang(id, ngaygiao, trangthai);
        }


        public Task<List<DonHangChiTiet>> GetDonHangChiTiets(int id)
        {
            return _respository.GetDonHangChiTiets(id);
        }

        public Task<List<DonHangChiTiet>> GetDonHangChiTiets()
        {
            return _respository.GetDonHangChiTiets();
        }

        public Task<List<DonHang>> GetDonHangs()
        {
            return _respository.GetDonHangs();
        }
    }
}
