using System;
using System.Collections.Generic;

namespace TH03_BookList.Models;

public partial class DonDatHang
{
    public int Sdh { get; set; }

    public int? Mkh { get; set; }

    public DateTime NgayDatHang { get; set; }

    public decimal? TriGia { get; set; }

    public bool DaGiaoHang { get; set; }

    public DateTime? NgayGiaoHang { get; set; }

    public virtual ICollection<CtDatHang> CtDatHangs { get; set; } = new List<CtDatHang>();

    public virtual KhachHang? MkhNavigation { get; set; }
}
