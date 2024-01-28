using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Core.Migrations
{
    public partial class AddedIndividualImagestoGenresandBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconLink",
                table: "Genres",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "",
                comment: "Icon to display the category");

            migrationBuilder.AddColumn<string>(
                name: "BookCover",
                table: "Books",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "",
                comment: "Url for book cover");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookCover", "CurrentPrice" },
                values: new object[] { "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1681419689l/62792245.jpg", 12.48m });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1686001523l/123416690.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1684911482l/127280062.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookCover", "CurrentPrice" },
                values: new object[] { "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1543687940l/42046112.jpg", 14.99m });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1478144278i/2203._SX50_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1679758198l/122757697.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1687766651l/71872869.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1696146860l/60531420._SY475_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "BookCover", "CurrentPrice" },
                values: new object[] { "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1306787560i/1067._SY75_.jpg", 13.48m });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1598823299l/42844155._SY475_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1682375874l/123136728.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1672016255l/65213197.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1684816198l/75665914.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1686092541l/62919162._SY475_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1676201263i/2199._SY75_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1680211580l/65213204._SY475_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1680363772l/124935364.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1678479483l/62923490.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1617037342i/40779082._SY75_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1675867491l/101161005.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1664729357l/62848145._SY475_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1685353278l/123199658.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1677109121l/75260734._SY475_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1617037342i/40779082._SY75_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1691467586l/157560224._SY475_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1668782119l/40097951._SY475_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1677050625l/75293479.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "BookCover", "CurrentPrice" },
                values: new object[] { "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1589475583l/53205854._SY475_.jpg", 14.15m });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1327861115i/8664353._SY75_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1680590876l/124944200.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 31,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1545494980l/40916679._SY475_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 32,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1681790985l/123239368.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 33,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1600789291l/40864002._SY475_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 34,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1388185755i/35100._SY75_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 35,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1685350627l/123280215.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 36,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1642696843l/60168787._SY475_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 37,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1678532327l/123211121.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 38,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1626710416l/58446227.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 39,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1435759367i/19400._SY75_.jpg");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 40,
                column: "BookCover",
                value: "https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1685351764l/123505439.jpg");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                column: "IconLink",
                value: "https://cdn.iconscout.com/icon/premium/png-256-thumb/fantasy-1709964-1452291.png");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                column: "IconLink",
                value: "https://cdn.iconscout.com/icon/premium/png-256-thumb/mystery-2760841-2298220.png");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "IconLink",
                value: "https://e7.pngegg.com/pngimages/12/127/png-clipart-computer-icons-romance-film-amor-love-miscellaneous.png");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4,
                column: "IconLink",
                value: "https://cdn3.iconfinder.com/data/icons/movie-genres-1/500/movie-category-genres-categories_11-512.png");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5,
                column: "IconLink",
                value: "https://cdn1.iconfinder.com/data/icons/material-core/21/history-512.png");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconLink",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "BookCover",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrentPrice",
                value: 24.99m);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "CurrentPrice",
                value: 29.99m);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                column: "CurrentPrice",
                value: 26.99m);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28,
                column: "CurrentPrice",
                value: 28.30m);
        }
    }
}
