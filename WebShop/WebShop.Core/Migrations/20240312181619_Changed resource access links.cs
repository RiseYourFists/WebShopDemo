using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Core.Migrations
{
    public partial class Changedresourceaccesslinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                column: "IconLink",
                value: "https://storage.googleapis.com/book-shop-web-proj/mystery-icon.png");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "IconLink",
                value: "https://storage.googleapis.com/book-shop-web-proj/romance-icon.png");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5,
                column: "IconLink",
                value: "https://storage.googleapis.com/book-shop-web-proj/history-icon-v2.png");

            migrationBuilder.CreateIndex(
                name: "IX_PlacedOrders_UserId",
                table: "PlacedOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlacedOrders_AspNetUsers_UserId",
                table: "PlacedOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlacedOrders_AspNetUsers_UserId",
                table: "PlacedOrders");

            migrationBuilder.DropIndex(
                name: "IX_PlacedOrders_UserId",
                table: "PlacedOrders");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                column: "IconLink",
                value: "https://storage.cloud.google.com/book-shop-web-proj/mystery-icon.png");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "IconLink",
                value: "https://storage.cloud.google.com/book-shop-web-proj/romance-icon.png");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5,
                column: "IconLink",
                value: "https://storage.cloud.google.com/book-shop-web-proj/history-icon-v2.png");
        }
    }
}
