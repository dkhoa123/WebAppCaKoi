using System;
using System.Collections.Generic;

namespace CaKoi.Respository.Entities;

public partial class DonHang
{
    public int IddonHang { get; set; }

    public int? Idkh { get; set; }

    public DateOnly? NgayMua { get; set; }

    public DateOnly? NgayGiao { get; set; }

    public string? TrangThai { get; set; }

    public string? ChoDuyet { get; set; }
}
