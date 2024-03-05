using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public int IdnhanVien { get; set; }
        public int? IdtaiKhoan { get; set; }
        public string? TenNv { get; set; }
        public string? DiaChi { get; set; }
        public string? Sdt { get; set; }

        public virtual TaiKhoan? IdtaiKhoanNavigation { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
