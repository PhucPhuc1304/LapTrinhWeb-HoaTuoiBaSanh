using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class Role
    {
        public Role()
        {
            TaiKhoans = new HashSet<TaiKhoan>();
        }

        public string IdRole { get; set; } = null!;
        public string? NameRole { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
