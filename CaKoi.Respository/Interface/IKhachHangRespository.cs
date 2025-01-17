﻿using CaKoi.Respository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaKoi.Respository.Interface
{
    public interface IKhachHangRespository
    {
        Task<List<KhachHang>> GetKhachHangs();
        Boolean AddKhachHang(KhachHang model);
        Task<bool> CapnhatKH(int idkh, KhachHang kh);
        Task<bool> DeleteKH(int id);
        KhachHang GetKhachHangByTenTaiKhoan(string tenTaiKhoan);
        KhachHang GetUserByUsernameAndPassword(string username, string password);
        KhachHang GetKhachByID(int id);
    }
}
