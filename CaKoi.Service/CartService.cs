using CaKoi.Respository;
using CaKoi.Respository.Entities;
using CaKoi.Respository.Interface;
using CaKoi.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CaKoi.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRespository _cartRepository;
        private readonly ICaCoiRespository _caKoiRepository; // Nếu bạn cần truy cập thông tin Ca Koi
        private IKhachHangRespository _khachRespository;
        public CartService(ICartRespository cartRepository, ICaCoiRespository caKoiRepository, IKhachHangRespository khachRespository)
        {
            _cartRepository = cartRepository;
            _caKoiRepository = caKoiRepository;
            _khachRespository = khachRespository;
        }

        public void AddToCart(int idkh, int id, int quantity)
        {
            var khach = _khachRespository.GetKhachByID(idkh);
            if (khach == null)
            {
                throw new InvalidOperationException("Khách hàng không tồn tại.");
            }

            var caKoi = _caKoiRepository.GetItemByCaKoiId(id);
            if (caKoi == null)
            {
                throw new InvalidOperationException("Cá koi không tồn tại.");
            }
            if (caKoi != null)
            {
                var donHangChiTiet = new DonHangChiTiet
                {
                    Idkh = khach.Idkh,
                    IdcaKoi = caKoi.IdcaKoi,
                    SoLuong = quantity,
                    Gia = caKoi.Gia,
                    TenLoai = caKoi.TenLoai,
                    TongTien = caKoi.Gia * quantity
                };

                // Kiểm tra nếu đối tượng này đã tồn tại trong giỏ hàng
                var existingItem = _cartRepository.GetItemByCaKoiId(caKoi.IdcaKoi);
                if (existingItem != null)
                {
                    // Nếu đã tồn tại, có thể cập nhật số lượng
                    existingItem.SoLuong += quantity;
                    existingItem.TongTien = existingItem.SoLuong * existingItem.Gia;
                    _cartRepository.UpdateCartItem(existingItem);
                }
                else
                {
                    // Nếu chưa tồn tại, thêm mới
                    _cartRepository.AddToCart(donHangChiTiet);
                }
            }
        }

        public void Deletecart(int id)
        {
            _cartRepository.Deletecart(id);
            
        }

        public IEnumerable<DonHangChiTiet> GetCartItems()
        {
            return _cartRepository.GetCartItems();
        }

        public decimal GetTotal()
        {
            return _cartRepository.GetTotal();
        }
    }
}
