using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class TaiKhoan
    {
        public TaiKhoan()
        {
            KhachHangs = new HashSet<KhachHang>();
            NhanViens = new HashSet<NhanVien>();
        }

        public int IdtaiKhoan { get; set; }
        public string? IdRole { get; set; }
        public string UserName { get; set; } = null!;
        public string Pasword { get; set; } = null!;

        public virtual Role? IdRoleNavigation { get; set; }
        public virtual ICollection<KhachHang> KhachHangs { get; set; }
        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
