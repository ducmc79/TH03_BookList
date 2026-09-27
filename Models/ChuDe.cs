using System;
using System.Collections.Generic;

namespace TH03_BookList.Models;

public partial class ChuDe
{
    public int Mcd { get; set; }

    public string? TenChuDe { get; set; }

    public int? Pid { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
