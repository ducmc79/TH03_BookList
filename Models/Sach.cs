using System;
using System.Collections.Generic;

namespace TH03_BookList.Models;

public partial class Sach
{
    public int Ms { get; set; }

    public string TenSach { get; set; } = null!;

    public decimal? DonGia { get; set; }

    public string? DonViTinh { get; set; }

    public string? MoTa { get; set; }

    public string? HinhMinhHoa { get; set; }

    public int? Mcd { get; set; }

    public int? Mnxb { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public int? SoLuongBan { get; set; }

    public int? SoLanXem { get; set; }

    public virtual ICollection<CtDatHang> CtDatHangs { get; set; } = new List<CtDatHang>();

    public virtual ChuDe? McdNavigation { get; set; }

    public virtual NhaXuatBan? MnxbNavigation { get; set; }

    public virtual ICollection<ThamGium> ThamGia { get; set; } = new List<ThamGium>();
}
