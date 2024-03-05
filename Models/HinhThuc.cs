using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class HinhThuc
    {
        public HinhThuc()
        {
            Cthds = new HashSet<Cthd>();
        }

        public string MaHt { get; set; } = null!;
        public string? TenHinhThuc { get; set; }

        public virtual ICollection<Cthd> Cthds { get; set; }
    }
}
