using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class Cthd
    {
        public int? SoLuong { get; set; }
        public double? DonGia { get; set; }
        public string MaHang { get; set; } = null!;
        public string? MaHt { get; set; }
        public int IdhoaDon { get; set; }

        public virtual HoaDon IdhoaDonNavigation { get; set; } = null!;
        public virtual HangHoa MaHangNavigation { get; set; } = null!;
        public virtual HinhThuc? MaHtNavigation { get; set; }
    }
}
