using System;
using System.Collections.Generic;

namespace TH03_BookList.Models;

public partial class KhachHang
{
    public int Mkh { get; set; }

    public string HoTen { get; set; } = null!;

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public DateTime? NgaySinh { get; set; }

    public bool GioiTinh { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<DonDatHang> DonDatHangs { get; set; } = new List<DonDatHang>();
}
