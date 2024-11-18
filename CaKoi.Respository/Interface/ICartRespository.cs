using CaKoi.Respository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaKoi.Respository.Interface
{
    public interface ICartRespository
    {
        void AddToCart(DonHangChiTiet donHangChiTiet);
        IEnumerable<DonHangChiTiet> GetCartItems();
        DonHangChiTiet GetItemByCaKoiId(int idkh, int idcaKoi);
        void UpdateCartItem(DonHangChiTiet dct);
        void Deletecart(int id);
        decimal GetTotal();
        decimal GetTotal(int id);
    }
}
