using System;
using System.Collections.Generic;

namespace TH03_BookList.Models;

public partial class CtDatHang
{
    public int Mctddh { get; set; }

    public int Sdh { get; set; }

    public int Ms { get; set; }

    public int? SoLuong { get; set; }

    public double? DonGia { get; set; }

    public double? ThanhTien { get; set; }

    public virtual Sach MsNavigation { get; set; } = null!;

    public virtual DonDatHang SdhNavigation { get; set; } = null!;
}
