using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Core.Migrations
{
    public partial class GenreImageChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 5,
                column: "IconLink",
                value: "https://cdn1.iconfinder.com/data/icons/material-core/21/history-512.png");
        }
    }
}
