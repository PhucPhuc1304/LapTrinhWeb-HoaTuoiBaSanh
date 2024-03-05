using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            HangHoas = new HashSet<HangHoa>();
        }

        public string MaNcc { get; set; } = null!;
        public string? TenNcc { get; set; }
        public string? DiaChiNcc { get; set; }
        public string? Sdtccc { get; set; }
        public string? EmailNcc { get; set; }

        public virtual ICollection<HangHoa> HangHoas { get; set; }
    }
}
