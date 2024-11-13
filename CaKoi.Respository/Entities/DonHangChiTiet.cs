using System;
using System.Collections.Generic;

namespace CaKoi.Respository.Entities;

public partial class DonHangChiTiet
{
    public int IddonHang { get; set; }

    public int IdcaKoi { get; set; }

    public int? SoLuong { get; set; }

    public double? TongTien { get; set; }

    public string? TenLoai { get; set; }

    public double? Gia { get; set; }

    public int Idkh { get; set; }
}
