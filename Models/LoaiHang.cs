using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class LoaiHang
    {
        public LoaiHang()
        {
            HangHoas = new HashSet<HangHoa>();
        }

        public string MaLoai { get; set; } = null!;
        public string? TenLoai { get; set; }
        public string? HinhAnh { get; set; }

        public virtual ICollection<HangHoa> HangHoas { get; set; }
    }
}
