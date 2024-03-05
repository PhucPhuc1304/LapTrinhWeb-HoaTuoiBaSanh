using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class LoaiKhach
    {
        public LoaiKhach()
        {
            KhachHangs = new HashSet<KhachHang>();
        }

        public string MaLoaiKhach { get; set; } = null!;
        public string? TenLoaiKhach { get; set; }
        public string? MaDonGia { get; set; }

        public virtual DonGium? MaDonGiaNavigation { get; set; }
        public virtual ICollection<KhachHang> KhachHangs { get; set; }
    }
}
