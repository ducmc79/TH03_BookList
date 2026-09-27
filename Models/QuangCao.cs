using System;
using System.Collections.Generic;

namespace TH03_BookList.Models;

public partial class QuangCao
{
    public int Stt { get; set; }

    public string? TenCty { get; set; }

    public string? HinhMinhHoa { get; set; }

    public string? Href { get; set; }

    public DateTime? NgayBatDau { get; set; }

    public DateTime? NgayHetHan { get; set; }
}
