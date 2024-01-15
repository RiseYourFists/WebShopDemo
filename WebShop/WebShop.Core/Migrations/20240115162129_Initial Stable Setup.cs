using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Core.Migrations
{
    public partial class InitialStableSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "User first name"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "User last name"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "Identifier if the user account is deleted"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "John Smith" },
                    { 2, "Alice Johnson" },
                    { 3, "Michael Brown" },
                    { 4, "Emily Davis" },
                    { 5, "Robert Miller" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fiction" },
                    { 2, "Mystery" },
                    { 3, "Romance" },
                    { 4, "Science Fiction" },
                    { 5, "History" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "BasePrice", "CurrentPrice", "Description", "GenreId", "Title" },
                values: new object[,]
                {
                    { 1, 1, 24.99m, 24.99m, "A thrilling adventure in an ancient city buried in the jungle.", 1, "The Lost City" },
                    { 2, 2, 19.99m, 19.99m, "Hercule Poirot solves a murder mystery on a luxurious train.", 2, "Murder on the Orient Express" },
                    { 3, 3, 14.99m, 14.99m, "A classic novel exploring the themes of love and social expectations.", 3, "Pride and Prejudice" },
                    { 4, 4, 29.99m, 29.99m, "Epic science fiction saga set in a distant future on the desert planet Arrakis.", 4, "Dune" },
                    { 5, 5, 22.99m, 22.99m, "An in-depth exploration of the rise and fall of the Roman Empire.", 5, "The History of Ancient Rome" },
                    { 6, 1, 18.99m, 18.99m, "A portrayal of the decadence of the Roaring Twenties in America.", 1, "The Great Gatsby" },
                    { 7, 2, 16.99m, 16.99m, "A powerful story addressing racial injustice in the American South.", 2, "To Kill a Mockingbird" },
                    { 8, 4, 27.99m, 27.99m, "Cyberpunk novel depicting a hacker's quest in a dystopian future.", 4, "Neuromancer" },
                    { 9, 3, 26.99m, 26.99m, "A monumental work chronicling the impact of the Napoleonic Wars.", 5, "War and Peace" },
                    { 10, 5, 20.99m, 20.99m, "A coming-of-age novel capturing the disillusionment of adolescence.", 1, "The Catcher in the Rye" },
                    { 11, 1, 28.50m, 28.50m, "A fantasy adventure novel set in the fictional world of Middle-earth.", 1, "The Hobbit" },
                    { 12, 2, 23.75m, 23.75m, "A gripping mystery involving a journalist and a hacker.", 2, "The Girl with the Dragon Tattoo" },
                    { 13, 3, 17.25m, 17.25m, "Jane Austen's novel exploring the challenges of love and societal expectations.", 3, "Sense and Sensibility" },
                    { 14, 4, 31.20m, 31.20m, "A space exploration odyssey that delves into the mysteries of the cosmos.", 4, "2001: A Space Odyssey" },
                    { 15, 5, 19.80m, 19.80m, "An ancient Chinese treatise on military strategy and tactics.", 5, "The Art of War" },
                    { 16, 1, 24.90m, 24.90m, "A dystopian novel depicting a futuristic world with advanced technology.", 1, "Brave New World" },
                    { 17, 2, 21.30m, 21.30m, "A classic novel portraying the struggles of a family during the Great Depression.", 2, "The Grapes of Wrath" },
                    { 18, 4, 29.45m, 29.45m, "A science fiction novel exploring the virtual and real worlds.", 4, "Snow Crash" },
                    { 19, 3, 27.60m, 27.60m, "Victor Hugo's epic novel following the lives of several characters in post-revolutionary France.", 5, "Les Misérables" },
                    { 20, 5, 30.75m, 30.75m, "A trilogy narrating the journey to destroy the One Ring and save Middle-earth.", 1, "The Lord of the Rings" },
                    { 21, 1, 26.85m, 26.85m, "A psychological horror novel set in an isolated, haunted hotel.", 2, "The Shining" },
                    { 22, 3, 18.55m, 18.55m, "Jane Austen's novel portraying the humorous adventures of a young woman.", 3, "Emma" },
                    { 23, 4, 32.10m, 32.10m, "The first book in Isaac Asimov's Foundation series, exploring the collapse of a galactic empire.", 4, "Foundation" },
                    { 24, 5, 20.15m, 20.15m, "A political treatise by Niccolò Machiavelli on leadership and power.", 5, "The Prince" },
                    { 25, 2, 22.40m, 22.40m, "Charles Dickens' novel following the life and education of an orphan.", 1, "The Great Expectations" },
                    { 26, 2, 25.15m, 25.15m, "A dystopian novel depicting a totalitarian society and the consequences of thought control.", 2, "1984" },
                    { 27, 3, 16.70m, 16.70m, "Nathaniel Hawthorne's novel exploring themes of sin and redemption.", 3, "The Scarlet Letter" },
                    { 28, 4, 28.30m, 28.30m, "A cyberpunk novel where consciousness can be transferred between bodies.", 4, "Altered Carbon" },
                    { 29, 1, 24.65m, 24.65m, "An ancient Greek epic poem attributed to Homer, chronicling the adventures of Odysseus.", 5, "The Odyssey" },
                    { 30, 5, 19.35m, 19.35m, "Mark Twain's novel following Huck's journey down the Mississippi River.", 1, "The Adventures of Huckleberry Finn" },
                    { 31, 1, 26.25m, 26.25m, "Gabriel García Márquez's magical realist novel spanning seven generations.", 2, "One Hundred Years of Solitude" },
                    { 32, 3, 21.95m, 21.95m, "Emily Brontë's novel depicting the passionate and destructive love between Heathcliff and Catherine.", 3, "Wuthering Heights" },
                    { 33, 4, 29.80m, 29.80m, "A comedic science fiction series following the misadventures of Arthur Dent.", 4, "The Hitchhikers Guide to the Galaxy" },
                    { 34, 5, 27.15m, 27.15m, "A series of personal writings by Roman Emperor Marcus Aurelius.", 5, "Meditations" },
                    { 35, 2, 30.40m, 30.40m, "Charles Dickens' historical novel set in London and Paris before and during the French Revolution.", 1, "A Tale of Two Cities" },
                    { 36, 2, 23.50m, 23.50m, "A post-apocalyptic novel following a father and son's journey through a desolate landscape.", 2, "The Road" },
                    { 37, 3, 18.75m, 18.75m, "Leo Tolstoy's novel exploring themes of love, infidelity, and societal expectations.", 3, "Anna Karenina" },
                    { 38, 4, 32.00m, 32.00m, "A science fiction novel depicting an astronaut's struggle for survival on Mars.", 4, "The Martian" },
                    { 39, 1, 20.60m, 20.60m, "Fyodor Dostoevsky's psychological novel exploring guilt and redemption.", 5, "Crime and Punishment" },
                    { 40, 5, 22.90m, 22.90m, "Oscar Wilde's novel exploring the consequences of a man's pursuit of eternal youth and beauty.", 1, "The Picture of Dorian Gray" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuthorPromotions");

            migrationBuilder.DropTable(
                name: "GenrePromotions");

            migrationBuilder.DropTable(
                name: "PlacedOrderBooks");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

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
