using System;
using System.Collections.Generic;

namespace TH03_BookList.Models;

public partial class NhaXuatBan
{
    public int Mnxb { get; set; }

    public string? TenNhaXuatBan { get; set; }

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
