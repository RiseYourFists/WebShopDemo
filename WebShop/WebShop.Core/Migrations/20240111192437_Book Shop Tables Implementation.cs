using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Core.Migrations
{
    public partial class BookShopTablesImplementation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Key identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false, comment: "Author pseudo-name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                },
                comment: "Author table");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Key identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false, comment: "Genre name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                },
                comment: "Genre table");

            migrationBuilder.CreateTable(
                name: "PlacedOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Key identifier"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Identity user id"),
                    DatePlaced = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date of order"),
                    DateFulfilled = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date of the order fulfillment"),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Total price for the order")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacedOrders", x => x.Id);
                },
                comment: "Placed order");

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Id identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Promotion Name"),
                    DiscountPercent = table.Column<double>(type: "float", nullable: false, comment: "Discount percent"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Start of promotion date"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "End of promotion date")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                },
                comment: "Promotion table");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Key identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false, comment: "Book title"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Book description"),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Book's non-promotional price"),
                    CurrentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Book's price with or without promotion"),
                    CoverPhoto = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false, comment: "Book's cover photo"),
                    GenreId = table.Column<int>(type: "int", nullable: false, comment: "Genre key identifier"),
                    AuthorId = table.Column<int>(type: "int", nullable: false, comment: "Author key identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Book table");

            migrationBuilder.CreateTable(
                name: "AuthorPromotions",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false, comment: "Author key identifier"),
                    PromotionId = table.Column<int>(type: "int", nullable: false, comment: "Promotion key identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorPromotions", x => new { x.AuthorId, x.PromotionId });
                    table.ForeignKey(
                        name: "FK_AuthorPromotions_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorPromotions_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Author targeted promotion");

            migrationBuilder.CreateTable(
                name: "GenrePromotions",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false, comment: "Promotion key identifier"),
                    GenreId = table.Column<int>(type: "int", nullable: false, comment: "Genre key identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenrePromotions", x => new { x.GenreId, x.PromotionId });
                    table.ForeignKey(
                        name: "FK_GenrePromotions_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenrePromotions_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Promotion targeting genres");

            migrationBuilder.CreateTable(
                name: "PlacedOrderBooks",
                columns: table => new
                {
                    PlacedOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Order Identifier"),
                    BookId = table.Column<int>(type: "int", nullable: false, comment: "Book Identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacedOrderBooks", x => new { x.BookId, x.PlacedOrderId });
                    table.ForeignKey(
                        name: "FK_PlacedOrderBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlacedOrderBooks_PlacedOrders_PlacedOrderId",
                        column: x => x.PlacedOrderId,
                        principalTable: "PlacedOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Collection of all books that are ordered");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorPromotions_PromotionId",
                table: "AuthorPromotions",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GenrePromotions_PromotionId",
                table: "GenrePromotions",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacedOrderBooks_PlacedOrderId",
                table: "PlacedOrderBooks",
                column: "PlacedOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorPromotions");

            migrationBuilder.DropTable(
                name: "GenrePromotions");

            migrationBuilder.DropTable(
                name: "PlacedOrderBooks");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "PlacedOrders");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
