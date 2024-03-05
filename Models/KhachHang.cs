using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public int IdkhachHang { get; set; }
        public string? TenKh { get; set; }
        public string? DiaChi { get; set; }
        public string? Sdt { get; set; }
        public string? MaLoaiKhach { get; set; }
        public int? IdtaiKhoan { get; set; }
        public string? GioiTinh { get; set; }
        public string? Email { get; set; }

        public virtual TaiKhoan? IdtaiKhoanNavigation { get; set; }
        public virtual LoaiKhach? MaLoaiKhachNavigation { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
