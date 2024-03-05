using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class KhoHang
    {
        public KhoHang()
        {
            HangHoas = new HashSet<HangHoa>();
        }

        public string MaKho { get; set; } = null!;
        public string? TenKho { get; set; }
        public string? DiaChi { get; set; }
        public string? Sdt { get; set; }
        public string? KyHieu { get; set; }

        public virtual ICollection<HangHoa> HangHoas { get; set; }
    }
}
