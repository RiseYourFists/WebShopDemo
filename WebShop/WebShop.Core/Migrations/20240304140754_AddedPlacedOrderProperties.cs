using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Core.Migrations
{
    public partial class AddedPlacedOrderProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "PlacedOrders",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                comment: "Address of delivery.");

            migrationBuilder.AddColumn<string>(
                name: "CityId",
                table: "PlacedOrders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "City of delivery.");

            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                table: "PlacedOrders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "Country of delivery.");

            migrationBuilder.AddColumn<bool>(
                name: "IsFulfilled",
                table: "PlacedOrders",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indication if the order is fulfilled. Default value is set to false.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "PlacedOrders");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "PlacedOrders");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "PlacedOrders");

            migrationBuilder.DropColumn(
                name: "IsFulfilled",
                table: "PlacedOrders");
        }
    }
}
