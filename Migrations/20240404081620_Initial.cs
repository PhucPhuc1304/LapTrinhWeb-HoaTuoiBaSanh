using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CF_HOATUOIBASANH.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Price1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Price2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Price3 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Image1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Image2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Image3 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Image4 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_Customers_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    OrderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PayMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PayStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailOrders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailOrders", x => new { x.OrderID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_DetailOrders_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailOrders_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Các loại ly" },
                    { 2, "Các loại cúc" },
                    { 3, "Các loại môn" },
                    { 4, "Các loại hoa màu" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryID", "Description", "Description2", "Description3", "Image", "Image1", "Image2", "Image3", "Image4", "Price", "Price1", "Price2", "Price3", "ProductName", "ProductStatus", "ProductUnit" },
                values: new object[,]
                {
                    { 1, 2, "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp.", null, null, "/img/product/CUC001.jpg", null, null, null, null, 150000m, null, null, null, "Cúc chùm", "Sale", "Bó" },
                    { 2, 2, "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày.", null, null, "/img/product/Luoi2.jpg", null, null, null, null, 150000m, null, null, null, "Cúc lưới", "Sale", "Bó" },
                    { 3, 2, "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp.", null, null, "/img/product/CUC003.jpg", null, null, null, null, 150000m, null, null, null, "Cúc cánh dài", "Sale", "Bó" },
                    { 4, 4, "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày.", null, null, "/img/product/HuongDuong001.jpg", null, null, null, null, 180000m, null, null, null, "Hướng dương", "Sale", "Bó" },
                    { 5, 1, "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp.", null, null, "/img/product/LY001.jpg", null, null, null, null, 180000m, null, null, null, "Ly Ù Hồng", "Sale", "Bó" },
                    { 7, 1, "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày.", null, null, "/img/product/LY002.jpg", null, null, null, null, 180000m, null, null, null, "Ly Kép Hồng Nhạt", "Sale", "Bó" },
                    { 8, 1, "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp.", null, null, "/img/product/LY003.jpg", null, null, null, null, 185000m, null, null, null, "Ly Kép Hồng Đậm", "Sale", "Bó" },
                    { 9, 1, "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày.", null, null, "/img/product/LY005.jpg", null, null, null, null, 185000m, null, null, null, "Kép Sen", "New", "Bó" },
                    { 10, 1, "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp.", null, null, "/img/product/LY006.jpg", null, null, null, null, 185000m, null, null, null, "Ly Kép Hồng Nhạt", "New", "Bó" },
                    { 11, 1, "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày.", null, null, "/img/product/LY007.jpg", null, null, null, null, 185000m, null, null, null, "Ly Kép Hồng Nhạt", "New", "Bó" },
                    { 12, 1, "Hoa lily còn có tên gọi là hoa bách hợp, trong từ Bách niên hảo hợp mang ý nghĩa trăm năm hạnh phúc hòa hợp.", null, null, "/img/product/LY008.jpg", null, null, null, null, 185000m, null, null, null, "Ly Kép Hồng Nhạt", "New", "Bó" },
                    { 13, 3, "Với hương thơm dễ chịu và những cánh hoa nở chồng lên nhau lại càng tăng thêm ý nghĩa thuận hoà, vì vậy mà hoa ly kép thường được ưa chuộng để trưng bày trong những ngày đầu năm mới. Đặc biệt hoa lily kép có thể giữ độ tươi lâu 7 – 10 ngày.", null, null, "/img/product/MON001.jpg", null, null, null, null, 60000m, null, null, null, "Môn trắng", "New", "Bó" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleID",
                table: "Accounts",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AccountID",
                table: "Customers",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_DetailOrders_ProductID",
                table: "DetailOrders",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailOrders");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
