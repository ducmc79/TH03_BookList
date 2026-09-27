using System;
using System.Collections.Generic;

namespace TH03_BookList.Models;

public partial class TacGium
{
    public int Mtg { get; set; }

    public string? TenTacGia { get; set; }

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public virtual ICollection<ThamGium> ThamGia { get; set; } = new List<ThamGium>();
}
