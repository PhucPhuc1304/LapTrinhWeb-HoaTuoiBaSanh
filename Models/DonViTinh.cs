using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class DonViTinh
    {
        public DonViTinh()
        {
            HangHoas = new HashSet<HangHoa>();
        }

        public string MaDvt { get; set; } = null!;
        public string? TenDvt { get; set; }

        public virtual ICollection<HangHoa> HangHoas { get; set; }
    }
}
