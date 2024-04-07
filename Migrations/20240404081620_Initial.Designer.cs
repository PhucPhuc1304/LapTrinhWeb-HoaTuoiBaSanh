﻿// <auto-generated />
using System;
using CF_HOATUOIBASANH.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CF_HOATUOIBASANH.Migrations
{
    [DbContext(typeof(HoaTuoiBaSanhContext))]
    [Migration("20240404081620_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HoaTuoiBaSanh_Core6.Models.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountID"), 1L, 1);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AccountID");

                    b.HasIndex("RoleID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("HoaTuoiBaSanh_Core6.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryID = 1,
                            CategoryName = "Các loại ly"
                        },
                        new
                        {
                            CategoryID = 2,
                            CategoryName = "Các loại cúc"
                        },
                        new
                        {
                            CategoryID = 3,
                            CategoryName = "Các loại môn"
                        },
                        new
                        {
                            CategoryID = 4,
                            CategoryName = "Các loại hoa màu"
                        });
                });

            modelBuilder.Entity("HoaTuoiBaSanh_Core6.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"), 1L, 1);

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CustomerID");

                    b.HasIndex("AccountID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("HoaTuoiBaSanh_Core6.Models.DetailOrder", b =>
                {
                    b.Property<int>("OrderID")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("ProductID")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int?>("Quantity")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("OrderID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("DetailOrders");
                });

            modelBuilder.Entity("HoaTuoiBaSanh_Core6.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<string>("OrderName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PayMethod")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PayStatus")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal?>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("HoaTuoiBaSanh_Core6.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"), 1L, 1);

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Image1")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Image2")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Image3")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Image4")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Price1")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Price2")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Price3")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ProductStatus")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ProductUnit")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductID = 1,
                            CategoryID = 2,
                            Description = "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp.",
                            Image = "/img/product/CUC001.jpg",
                            Price = 150000m,
                            ProductName = "Cúc chùm",
                            ProductStatus = "Sale",
                            ProductUnit = "Bó"
                        },
                        new
                        {
                            ProductID = 2,
                            CategoryID = 2,
                            Description = "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày.",
                            Image = "/img/product/Luoi2.jpg",
                            Price = 150000m,
                            ProductName = "Cúc lưới",
                            ProductStatus = "Sale",
                            ProductUnit = "Bó"
                        },
                        new
                        {
                            ProductID = 3,
                            CategoryID = 2,
                            Description = "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp.",
                            Image = "/img/product/CUC003.jpg",
                            Price = 150000m,
                            ProductName = "Cúc cánh dài",
                            ProductStatus = "Sale",
                            ProductUnit = "Bó"
                        },
                        new
                        {
                            ProductID = 4,
                            CategoryID = 4,
                            Description = "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày.",
                            Image = "/img/product/HuongDuong001.jpg",
                            Price = 180000m,
                            ProductName = "Hướng dương",
                            ProductStatus = "Sale",
                            ProductUnit = "Bó"
                        },
                        new
                        {
                            ProductID = 5,
                            CategoryID = 1,
                            Description = "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp.",
                            Image = "/img/product/LY001.jpg",
                            Price = 180000m,
                            ProductName = "Ly Ù Hồng",
                            ProductStatus = "Sale",
                            ProductUnit = "Bó"
                        },
                        new
                        {
                            ProductID = 7,
                            CategoryID = 1,
                            Description = "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày.",
                            Image = "/img/product/LY002.jpg",
                            Price = 180000m,
                            ProductName = "Ly Kép Hồng Nhạt",
                            ProductStatus = "Sale",
                            ProductUnit = "Bó"
                        },
                        new
                        {
                            ProductID = 8,
                            CategoryID = 1,
                            Description = "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp.",
                            Image = "/img/product/LY003.jpg",
                            Price = 185000m,
                            ProductName = "Ly Kép Hồng Đậm",
                            ProductStatus = "Sale",
                            ProductUnit = "Bó"
                        },
                        new
                        {
                            ProductID = 9,
                            CategoryID = 1,
                            Description = "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày.",
                            Image = "/img/product/LY005.jpg",
                            Price = 185000m,
                            ProductName = "Kép Sen",
                            ProductStatus = "New",
                            ProductUnit = "Bó"
                        },
                        new
                        {
                            ProductID = 10,
                            CategoryID = 1,
                            Description = "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp.",
                            Image = "/img/product/LY006.jpg",
                            Price = 185000m,
                            ProductName = "Ly Kép Hồng Nhạt",
                            ProductStatus = "New",
                            ProductUnit = "Bó"
                        },
                        new
                        {
                            ProductID = 11,
                            CategoryID = 1,
                            Description = "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày.",
                            Image = "/img/product/LY007.jpg",
                            Price = 185000m,
                            ProductName = "Ly Kép Hồng Nhạt",
                            ProductStatus = "New",
                            ProductUnit = "Bó"
                        },
                        new
                        {
                            ProductID = 12,
                            CategoryID = 1,
                            Description = "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp.",
                            Image = "/img/product/LY008.jpg",
                            Price = 185000m,
                            ProductName = "Ly Kép Hồng Nhạt",
                            ProductStatus = "New",
                            ProductUnit = "Bó"
                        },
                        new
                        {
                            ProductID = 13,
                            CategoryID = 3,
                            Description = "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày.",
                            Image = "/img/product/MON001.jpg",
                            Price = 60000m,
                            ProductName = "Môn trắng",
                            ProductStatus = "New",
                            ProductUnit = "Bó"
                        });
                });

            modelBuilder.Entity("HoaTuoiBaSanh_Core6.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("HoaTuoiBaSanh_Core6.Models.Account", b =>
                {
                    b.HasOne("HoaTuoiBaSanh_Core6.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("HoaTuoiBaSanh_Core6.Models.Customer", b =>
                {
                    b.HasOne("HoaTuoiBaSanh_Core6.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("HoaTuoiBaSanh_Core6.Models.DetailOrder", b =>
                {
                    b.HasOne("HoaTuoiBaSanh_Core6.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HoaTuoiBaSanh_Core6.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("HoaTuoiBaSanh_Core6.Models.Order", b =>
                {
                    b.HasOne("HoaTuoiBaSanh_Core6.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("HoaTuoiBaSanh_Core6.Models.Product", b =>
                {
                    b.HasOne("HoaTuoiBaSanh_Core6.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}