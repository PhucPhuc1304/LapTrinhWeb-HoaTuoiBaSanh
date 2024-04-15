using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CF_HOATUOIBASANH.Migrations
{
    public partial class UpdateShipCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ShipCost",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipCost",
                table: "Orders");
        }
    }
}
