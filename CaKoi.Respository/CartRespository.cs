﻿using CaKoi.Respository.Entities;
using CaKoi.Respository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
  
namespace CaKoi.Respository
{
    public class CartRespository : ICartRespository
    {
        public readonly CaKoiContext _db;
        public CartRespository(CaKoiContext db)
        {
            _db = db;
        }
        public void AddToCart(DonHangChiTiet donHangChiTiet)
        {
           _db.DonHangChiTiets.AddAsync(donHangChiTiet);
           _db.SaveChangesAsync();
        }

        public void Deletecart(int id)
        {
                var dh = _db.DonHangChiTiets.FirstOrDefault(i => i.IdcaKoi == id);
            if (dh != null)
                {
                    _db.Remove(dh);
                    _db.SaveChanges();
                }
        }

        public IEnumerable<DonHangChiTiet> GetCartItems()
        {
            return _db.DonHangChiTiets.ToList();
        }

        public DonHangChiTiet GetItemByCaKoiId(int idkh, int idcaKoi)
        {
            return _db.DonHangChiTiets.FirstOrDefault(d => d.Idkh == idkh && d.IdcaKoi == idcaKoi);
        }

        public decimal GetTotal()
        {
            return (decimal)_db.DonHangChiTiets.Sum(item => item.TongTien);
        }

        public decimal GetTotal(int id)
        {
            return (decimal)_db.DonHangChiTiets.Where(i => i.Idkh == id).Sum(item => item.TongTien);
        }

        public void UpdateCartItem(DonHangChiTiet dct)
        {
            var existingItem = _db.DonHangChiTiets
        .FirstOrDefault(d => d.IdcaKoi == dct.IdcaKoi && d.Idkh == dct.Idkh);
            if (existingItem != null)
            {
                existingItem.SoLuong = dct.SoLuong;
                existingItem.TongTien = dct.TongTien;
                _db.SaveChanges();
            }
        }
    }
}
