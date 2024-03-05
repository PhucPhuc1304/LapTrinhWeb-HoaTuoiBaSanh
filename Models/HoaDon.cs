using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            Cthds = new HashSet<Cthd>();
        }

        public int IdhoaDon { get; set; }
        public DateTime? NgayLapHd { get; set; }
        public int? MaNv { get; set; }
        public int? IdkhachHang { get; set; }

        public virtual KhachHang? IdkhachHangNavigation { get; set; }
        public virtual NhanVien? MaNvNavigation { get; set; }
        public virtual ICollection<Cthd> Cthds { get; set; }
    }
}
