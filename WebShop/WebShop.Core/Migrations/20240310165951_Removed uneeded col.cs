using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Core.Migrations
{
    public partial class Removeduneededcol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFulfilled",
                table: "PlacedOrders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFulfilled",
                table: "PlacedOrders",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indication if the order is fulfilled. Default value is set to false.");
        }
    }
}
