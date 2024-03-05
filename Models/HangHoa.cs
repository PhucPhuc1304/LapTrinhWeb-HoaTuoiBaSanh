using System;
using System.Collections.Generic;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class HangHoa
    {
        public HangHoa()
        {
            Cthds = new HashSet<Cthd>();
        }

        public double? GiaNhap { get; set; }
        public string MaHang { get; set; } = null!;
        public string? TenHang { get; set; }
        public string? MaLoai { get; set; }
        public string? MaDvt { get; set; }
        public string? MaNcc { get; set; }
        public string? MaKho { get; set; }
        public double? DonGia1 { get; set; }
        public double? DonGia2 { get; set; }
        public double? DonGia3 { get; set; }
        public double? DonGia4 { get; set; }
        public double? GiaLe { get; set; }
        public string? HinhAnh { get; set; }
        public string? MoTa { get; set; }
        public string? HinhAnh2 { get; set; }
        public string? HinhAnh3 { get; set; }
        public string? HinhAnh4 { get; set; }
        public string? MoTa1 { get; set; }
        public string? MoTa2 { get; set; }
        public string? TrangThai { get; set; }

        public virtual DonViTinh? MaDvtNavigation { get; set; }
        public virtual KhoHang? MaKhoNavigation { get; set; }
        public virtual LoaiHang? MaLoaiNavigation { get; set; }
        public virtual NhaCungCap? MaNccNavigation { get; set; }
        public virtual ICollection<Cthd> Cthds { get; set; }
    }
}
