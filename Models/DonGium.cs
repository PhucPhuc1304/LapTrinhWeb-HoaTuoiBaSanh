using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class DonGium
    {
        public DonGium()
        {
            LoaiKhaches = new HashSet<LoaiKhach>();
        }

        public string MaDonGia { get; set; } = null!;
        public string? TenDonGia { get; set; }

        public virtual ICollection<LoaiKhach> LoaiKhaches { get; set; }
    }
}
