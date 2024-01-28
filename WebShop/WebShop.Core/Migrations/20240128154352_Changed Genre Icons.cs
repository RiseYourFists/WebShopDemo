using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Core.Migrations
{
    public partial class ChangedGenreIcons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                column: "IconLink",
                value: "https://thenounproject.com/api/private/icons/3395015/edit/?backgroundShape=SQUARE&backgroundShapeColor=%23000000&backgroundShapeOpacity=0&exportSize=752&flipX=false&flipY=false&foregroundColor=%23353535&foregroundOpacity=1&imageFormat=png&rotation=0");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "IconLink",
                value: "https://thenounproject.com/api/private/icons/429438/edit/?backgroundShape=SQUARE&backgroundShapeColor=%23000000&backgroundShapeOpacity=0&exportSize=752&flipX=false&flipY=false&foregroundColor=%23353535&foregroundOpacity=1&imageFormat=png&rotation=0");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5,
                column: "IconLink",
                value: "https://thenounproject.com/api/private/icons/2484315/edit/?backgroundShape=SQUARE&backgroundShapeColor=%23000000&backgroundShapeOpacity=0&exportSize=752&flipX=false&flipY=false&foregroundColor=%23353535&foregroundOpacity=1&imageFormat=png&rotation=0");
        }
    }
}
