using System;
using System.Collections.Generic;

namespace TH03_BookList.Models;

public partial class ThamGium
{
    public int Ms { get; set; }

    public int Mtg { get; set; }

    public string? VaiTro { get; set; }

    public virtual Sach MsNavigation { get; set; } = null!;

    public virtual TacGium MtgNavigation { get; set; } = null!;
}
