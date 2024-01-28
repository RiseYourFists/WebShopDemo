using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Core.Migrations
{
    public partial class BookCoverChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "BookCover",
                value: "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1657555929i/27323.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1306787560i/1067._SY75_.jpg");
        }
    }
}
