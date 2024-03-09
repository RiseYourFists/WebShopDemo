using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Core.Migrations
{
    public partial class Placed_OrderInfo_Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "PlacedOrders");

            migrationBuilder.AddColumn<bool>(
                name: "IsShipped",
                table: "PlacedOrders",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indication of order stage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShipped",
                table: "PlacedOrders");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "PlacedOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "Total price for the order");
        }
    }
}
