using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HoaTuoiBaSanh_Core6.Models
{
    public partial class webContext : DbContext
    {
        public webContext()
        {
        }

        public webContext(DbContextOptions<webContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cthd> Cthds { get; set; } = null!;
        public virtual DbSet<DonGium> DonGia { get; set; } = null!;
        public virtual DbSet<DonViTinh> DonViTinhs { get; set; } = null!;
        public virtual DbSet<HangHoa> HangHoas { get; set; } = null!;
        public virtual DbSet<HinhThuc> HinhThucs { get; set; } = null!;
        public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<KhoHang> KhoHangs { get; set; } = null!;
        public virtual DbSet<LoaiHang> LoaiHangs { get; set; } = null!;
        public virtual DbSet<LoaiKhach> LoaiKhaches { get; set; } = null!;
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-HINEBV60;Database=web;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cthd>(entity =>
            {
                entity.HasKey(e => new { e.MaHang, e.IdhoaDon });

                entity.ToTable("CTHD");

                entity.Property(e => e.MaHang).HasMaxLength(10);

                entity.Property(e => e.IdhoaDon).HasColumnName("IDHoaDon");

                entity.Property(e => e.MaHt)
                    .HasMaxLength(10)
                    .HasColumnName("MaHT");

                entity.HasOne(d => d.IdhoaDonNavigation)
                    .WithMany(p => p.Cthds)
                    .HasForeignKey(d => d.IdhoaDon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship17");

                entity.HasOne(d => d.MaHangNavigation)
                    .WithMany(p => p.Cthds)
                    .HasForeignKey(d => d.MaHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship16");

                entity.HasOne(d => d.MaHtNavigation)
                    .WithMany(p => p.Cthds)
                    .HasForeignKey(d => d.MaHt)
                    .HasConstraintName("Relationship18");
            });

            modelBuilder.Entity<DonGium>(entity =>
            {
                entity.HasKey(e => e.MaDonGia);

                entity.Property(e => e.MaDonGia).HasMaxLength(10);

                entity.Property(e => e.TenDonGia).HasMaxLength(50);
            });

            modelBuilder.Entity<DonViTinh>(entity =>
            {
                entity.HasKey(e => e.MaDvt);

                entity.ToTable("DonViTinh");

                entity.Property(e => e.MaDvt)
                    .HasMaxLength(10)
                    .HasColumnName("MaDVT");

                entity.Property(e => e.TenDvt)
                    .HasMaxLength(10)
                    .HasColumnName("TenDVT");
            });

            modelBuilder.Entity<HangHoa>(entity =>
            {
                entity.HasKey(e => e.MaHang);

                entity.ToTable("HangHoa");

                entity.Property(e => e.MaHang).HasMaxLength(10);

                entity.Property(e => e.HinhAnh).HasMaxLength(200);

                entity.Property(e => e.HinhAnh2).HasMaxLength(200);

                entity.Property(e => e.HinhAnh3).HasMaxLength(200);

                entity.Property(e => e.HinhAnh4).HasMaxLength(200);

                entity.Property(e => e.MaDvt)
                    .HasMaxLength(10)
                    .HasColumnName("MaDVT");

                entity.Property(e => e.MaKho).HasMaxLength(20);

                entity.Property(e => e.MaLoai).HasMaxLength(20);

                entity.Property(e => e.MaNcc)
                    .HasMaxLength(10)
                    .HasColumnName("MaNCC");

                entity.Property(e => e.TrangThai).HasMaxLength(15);

                entity.HasOne(d => d.MaDvtNavigation)
                    .WithMany(p => p.HangHoas)
                    .HasForeignKey(d => d.MaDvt)
                    .HasConstraintName("Relationship5");

                entity.HasOne(d => d.MaKhoNavigation)
                    .WithMany(p => p.HangHoas)
                    .HasForeignKey(d => d.MaKho)
                    .HasConstraintName("Relationship7");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.HangHoas)
                    .HasForeignKey(d => d.MaLoai)
                    .HasConstraintName("Relationship4");

                entity.HasOne(d => d.MaNccNavigation)
                    .WithMany(p => p.HangHoas)
                    .HasForeignKey(d => d.MaNcc)
                    .HasConstraintName("Relationship6");
            });



            modelBuilder.Entity<HinhThuc>(entity =>
            {
                entity.HasKey(e => e.MaHt);

                entity.ToTable("HinhThuc");

                entity.Property(e => e.MaHt)
                    .HasMaxLength(10)
                    .HasColumnName("MaHT");

                entity.Property(e => e.TenHinhThuc).HasMaxLength(50);
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.IdhoaDon);

                entity.ToTable("HoaDon");

                entity.Property(e => e.IdhoaDon).HasColumnName("IDHoaDon");

                entity.Property(e => e.IdkhachHang).HasColumnName("IDKhachHang");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.NgayLapHd)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("NgayLapHD");

                entity.HasOne(d => d.IdkhachHangNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.IdkhachHang)
                    .HasConstraintName("Relationship13");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("Relationship15");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.IdkhachHang);

                entity.ToTable("KhachHang");

                entity.Property(e => e.IdkhachHang).HasColumnName("IDKhachHang");

                entity.Property(e => e.DiaChi).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.GioiTinh).HasMaxLength(10);

                entity.Property(e => e.IdtaiKhoan).HasColumnName("IDTaiKhoan");

                entity.Property(e => e.MaLoaiKhach).HasMaxLength(10);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(20)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenKh).HasMaxLength(200);

                entity.HasOne(d => d.IdtaiKhoanNavigation)
                    .WithMany(p => p.KhachHangs)
                    .HasForeignKey(d => d.IdtaiKhoan)
                    .HasConstraintName("Relationship21");

                entity.HasOne(d => d.MaLoaiKhachNavigation)
                    .WithMany(p => p.KhachHangs)
                    .HasForeignKey(d => d.MaLoaiKhach)
                    .HasConstraintName("Relationship3");
            });

            modelBuilder.Entity<KhoHang>(entity =>
            {
                entity.HasKey(e => e.MaKho);

                entity.ToTable("KhoHang");

                entity.Property(e => e.MaKho).HasMaxLength(20);

                entity.Property(e => e.DiaChi).HasMaxLength(500);

                entity.Property(e => e.KyHieu).HasMaxLength(7);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(20)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenKho).HasMaxLength(500);
            });

            modelBuilder.Entity<LoaiHang>(entity =>
            {
                entity.HasKey(e => e.MaLoai);

                entity.ToTable("LoaiHang");

                entity.Property(e => e.MaLoai).HasMaxLength(20);

                entity.Property(e => e.HinhAnh).HasMaxLength(200);

                entity.Property(e => e.TenLoai).HasMaxLength(60);
            });

            modelBuilder.Entity<LoaiKhach>(entity =>
            {
                entity.HasKey(e => e.MaLoaiKhach);

                entity.ToTable("LoaiKhach");

                entity.Property(e => e.MaLoaiKhach).HasMaxLength(10);

                entity.Property(e => e.MaDonGia).HasMaxLength(10);

                entity.Property(e => e.TenLoaiKhach).HasMaxLength(50);

                entity.HasOne(d => d.MaDonGiaNavigation)
                    .WithMany(p => p.LoaiKhaches)
                    .HasForeignKey(d => d.MaDonGia)
                    .HasConstraintName("Relationship19");
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.MaNcc);

                entity.ToTable("NhaCungCap");

                entity.Property(e => e.MaNcc)
                    .HasMaxLength(10)
                    .HasColumnName("MaNCC");

                entity.Property(e => e.DiaChiNcc)
                    .HasMaxLength(200)
                    .HasColumnName("DiaChiNCC");

                entity.Property(e => e.EmailNcc)
                    .HasMaxLength(200)
                    .HasColumnName("EmailNCC")
                    .IsFixedLength();

                entity.Property(e => e.Sdtccc)
                    .HasMaxLength(20)
                    .HasColumnName("SDTCCC");

                entity.Property(e => e.TenNcc).HasColumnName("TenNCC");
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.IdnhanVien);

                entity.ToTable("NhanVien");

                entity.Property(e => e.IdnhanVien).HasColumnName("IDNhanVien");

                entity.Property(e => e.DiaChi).HasMaxLength(500);

                entity.Property(e => e.IdtaiKhoan).HasColumnName("IDTaiKhoan");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(20)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenNv)
                    .HasMaxLength(200)
                    .HasColumnName("TenNV");

                entity.HasOne(d => d.IdtaiKhoanNavigation)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.IdtaiKhoan)
                    .HasConstraintName("Relationship20");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Role");

                entity.Property(e => e.IdRole).HasMaxLength(1);

                entity.Property(e => e.NameRole).HasMaxLength(10);
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.IdtaiKhoan);

                entity.ToTable("TaiKhoan");

                entity.Property(e => e.IdtaiKhoan).HasColumnName("IDTaiKhoan");

                entity.Property(e => e.IdRole).HasMaxLength(1);

                entity.Property(e => e.Pasword).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.TaiKhoans)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("Relationship11");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
