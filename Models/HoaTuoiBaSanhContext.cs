using Microsoft.EntityFrameworkCore;

namespace CF_HOATUOIBASANH.Models
{
    public partial class HoaTuoiBaSanhContext : DbContext
    {
        public HoaTuoiBaSanhContext() { }

        public HoaTuoiBaSanhContext(DbContextOptions<HoaTuoiBaSanhContext> options) : base(options) { }
        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<DetailOrder> DetailOrders { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetailOrder>()
              .HasKey(e => new {
                  e.OrderID,
                  e.ProductID
              });
            modelBuilder.Entity<Category>().HasData(
     new Category { CategoryID = 1, CategoryName = "Các loại ly" },
     new Category { CategoryID = 2, CategoryName = "Các loại cúc" },
     new Category { CategoryID = 3, CategoryName = "Các loại môn" },
     new Category { CategoryID = 4, CategoryName = "Các loại hoa màu" }
 );
            modelBuilder
                .Entity<Product>()
                .HasData(
                    new Product
                    { ProductID = 1,
                        ProductName = "Cúc chùm",
                        CategoryID = 2,
                        ProductUnit = "Bó",
                        ProductStatus = "Sale",
                        Price = 150000,
                        Image = "/img/product/CUC001.jpg",
                        Description =
                            "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp."
                    },
                    new Product
                    {   ProductID=2,
                        ProductName = "Cúc lưới",
                        CategoryID = 2,
                        ProductUnit = "Bó",
                        ProductStatus = "Sale",
                        Price = 150000,
                        Image = "/img/product/Luoi2.jpg",
                        Description =
                            "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày."
                    },
                    // Add more products as needed
                    new Product
                    {
                        ProductID=3,
                        ProductName = "Cúc cánh dài",
                        CategoryID = 2,
                        ProductUnit = "Bó",
                        ProductStatus = "Sale",
                        Price = 150000,
                        Image = "/img/product/CUC003.jpg",
                        Description =
                            "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp."
                    },
                    new Product
                    {   ProductID=  4,
                        ProductName = "Hướng dương",
                        CategoryID = 4,
                        ProductUnit = "Bó",
                        ProductStatus = "Sale",
                        Price = 180000,
                        Image = "/img/product/HuongDuong001.jpg",
                        Description =
                            "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày."
                    },
                    new Product
                    {   ProductID =5,
                        ProductName = "Ly Ù Hồng",
                        CategoryID = 1,
                        ProductUnit = "Bó",
                        ProductStatus = "Sale",
                        Price = 180000,
                        Image = "/img/product/LY001.jpg",
                        Description =
                            "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp."
                    },
                    new Product
                    {ProductID = 7,
                        ProductName = "Ly Kép Hồng Nhạt",
                        CategoryID = 1,
                        ProductUnit = "Bó",
                        ProductStatus = "Sale",
                        Price = 180000,
                        Image = "/img/product/LY002.jpg",
                        Description =
                            "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày."
                    },
                    new Product
                    {
                        ProductID = 8,
                        ProductName = "Ly Kép Hồng Đậm",
                        CategoryID = 1,
                        ProductUnit = "Bó",
                        ProductStatus = "Sale",
                        Price = 185000,
                        Image = "/img/product/LY003.jpg",
                        Description =
                            "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp."
                    },
                    // Product 8
                    new Product
                    {
                        ProductID= 9,
                        ProductName = "Kép Sen",
                        CategoryID = 1,
                        ProductUnit = "Bó",
                        ProductStatus = "New",
                        Price = 185000,
                        Image = "/img/product/LY005.jpg",
                        Description =
                            "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày."
                    },
                    // Product 9
                    new Product
                    {
                        ProductID= 10,
                        ProductName = "Ly Kép Hồng Nhạt",
                        CategoryID = 1,
                        ProductUnit = "Bó",
                        ProductStatus = "New",
                        Price = 185000,
                        Image = "/img/product/LY006.jpg",
                        Description =
                            "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp."
                    },
                    // Product 10
                    new Product
                    {   ProductID = 11,
                        ProductName = "Ly Kép Hồng Nhạt",
                        CategoryID = 1,
                        ProductUnit = "Bó",
                        ProductStatus = "New",
                        Price = 185000,
                        Image = "/img/product/LY007.jpg",
                        Description =
                            "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày."
                    },
                    // Product 11
                    new Product
                    {
                        ProductID= 12,
                        ProductName = "Ly Kép Hồng Nhạt",
                        CategoryID = 1,
                        ProductUnit = "Bó",
                        ProductStatus = "New",
                        Price = 185000,
                        Image = "/img/product/LY008.jpg",
                        Description =
                            "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp."
                    },
                    // Product 12
                    new Product
                    {
                        ProductID = 13,
                        ProductName = "Môn trắng",
                        CategoryID = 3,
                        ProductUnit = "Bó",
                        ProductStatus = "New",
                        Price = 60000,
                        Image = "/img/product/MON001.jpg",
                        Description =
                            "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày."
                    }
                );
            modelBuilder.Entity<Role>().HasData(
       new Role { RoleID = 1, RoleName = "User" },
       new Role { RoleID = 2, RoleName = "Admin" },
       new Role { RoleID = 3, RoleName = "Manager" }
   );





        }
    }

}