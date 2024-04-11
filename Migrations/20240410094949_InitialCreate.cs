using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CF_HOATUOIBASANH.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderName",
                table: "Orders",
                newName: "DeliveryMethod");

            migrationBuilder.AddColumn<string>(
                name: "ShipAddress",
                table: "Orders",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipAddress",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DeliveryMethod",
                table: "Orders",
                newName: "OrderName");
        }
    }
}
