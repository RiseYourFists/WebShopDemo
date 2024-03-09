using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Core.Migrations
{
    public partial class Column_Renaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "PlacedOrders",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "PlacedOrders",
                newName: "City");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Country",
                table: "PlacedOrders",
                newName: "CountryId");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "PlacedOrders",
                newName: "CityId");
        }
    }
}
