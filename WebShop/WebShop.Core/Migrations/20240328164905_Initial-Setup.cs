using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Core.Migrations
{
    public partial class InitialSetup : Migration
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
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false, comment: "Genre name"),
                    IconLink = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false, comment: "Icon to display the category")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                },
                comment: "Genre table");

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
                name: "PlacedOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Key identifier"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Identity user id"),
                    DatePlaced = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date of order"),
                    DateFulfilled = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Date of the order fulfillment"),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Country of delivery."),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "City of delivery."),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "Address of delivery."),
                    IsShipped = table.Column<bool>(type: "bit", nullable: false, comment: "Indication of order stage")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacedOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlacedOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Placed order");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Key identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Book title"),
                    Description = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false, comment: "Book description"),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Book's non-promotional price"),
                    StockQuantity = table.Column<int>(type: "int", nullable: false, comment: "Total quantity available"),
                    BookCover = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false, comment: "Url for book cover"),
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
                    BookId = table.Column<int>(type: "int", nullable: false, comment: "Book Identifier"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Quantity of books ordered"),
                    SingleItemPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Purchase price for single book")
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
                    { 1, "Stephen King" },
                    { 2, "Agatha Christie" },
                    { 3, "Danielle Steel" },
                    { 4, "Arthur C. Clarke" },
                    { 5, "Edwin A. Abbott" },
                    { 6, "J. R. R. Tolkien" },
                    { 7, "J. K. Rowling" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "IconLink", "Name" },
                values: new object[,]
                {
                    { 1, "https://cdn.iconscout.com/icon/premium/png-256-thumb/fantasy-1709964-1452291.png", "Fiction" },
                    { 2, "https://storage.googleapis.com/book-shop-web-proj/mystery-icon.png", "Mystery" },
                    { 3, "https://storage.googleapis.com/book-shop-web-proj/romance-icon.png", "Romance" },
                    { 4, "https://cdn3.iconfinder.com/data/icons/movie-genres-1/500/movie-category-genres-categories_11-512.png", "Science Fiction" },
                    { 5, "https://storage.googleapis.com/book-shop-web-proj/history-icon-v2.png", "History" },
                    { 6, "https://storage.googleapis.com/book-shop-web-proj/fantasy-edited.png", "Fantasy" }
                });

            migrationBuilder.InsertData(
                table: "Promotions",
                columns: new[] { "Id", "DiscountPercent", "EndDate", "Name", "StartDate" },
                values: new object[] { 1, 50.0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Year of fiction", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "BasePrice", "BookCover", "Description", "GenreId", "StockQuantity", "Title" },
                values: new object[,]
                {
                    { 1, 1, 19.99m, "https://images-us.bookshop.org/ingram/9781501181009.jpg?height=500&v=v2-a1d7cc03af48883b3234be483797e131", "Now an HBO limited series starring Ben Mendelsohn!​ Evil has many faces...maybe even yours in this #1 New York Times bestseller from master storyteller Stephen King. An eleven-year-old boy's violated corpse is discovered in a town park. Eyewitnesses and fingerprints point unmistakably to one of Flint City's most popular citizens--Terry Maitland, Little League coach, English teacher, husband, and father of two girls. Detective Ralph Anderson, whose son Maitland once coached, orders a quick and very public arrest. Maitland has an alibi, but Anderson and the district attorney soon have DNA evidence to go with the fingerprints and witnesses. Their case seems ironclad. As the investigation expands and horrifying details begin to emerge, King's story kicks into high gear, generating strong tension and almost unbearable suspense. Terry Maitland seems like a nice guy, but is he wearing another face? When the answer comes, it will shock you as only Stephen King can.", 2, 300, "The Outsider" },
                    { 2, 1, 41.99m, "https://images-us.bookshop.org/ingram/9780195115932.jpg?height=500&v=v2-800763754b3b545e876ade973b5a1cb4", "In Blood Thirst: 100 Years of Vampire Fiction, Wolf brings together over two dozen tales in which vampires of all varieties make their ghastly presence felt: male and female, human and non-human, humorous and heroic, all of them kin to the dreadful bat. From Lafcadio Hearn, Mary E. Wilkins-Freeman, and August Derleth to such contemporary masters as Anne Rice, Stephen King, Joyce Carol Oates, John Cheever, and Woody Allen, and in settings as diverse as rural New England and outer space, this collection offers readers a dazzling compendium of vampire stories.", 1, 300, "Blood Thirst: 100 Years of Vampire Fiction" },
                    { 3, 1, 9.99m, "https://images-us.bookshop.org/ingram/9781982102326.jpg?height=500&v=v2", "From legendary master storyteller Stephen King, a riveting story about \"an ordinary man in an extraordinary condition rising above hatred\" (The Washington Post) and bringing the fictional town of Castle Rock, Maine together--a \"joyful, uplifting\" (Entertainment Weekly) tale about finding common ground despite deep-rooted differences, \"the sign of a master elevating his own legendary game yet again\" (USA TODAY).\r\nAlthough Scott Carey doesn't look any different, he's been steadily losing weight. There are a couple of other odd things, too. He weighs the same in his clothes and out of them, no matter how heavy they are. Scott doesn't want to be poked and prodded. He mostly just wants someone else to know, and he trusts Doctor Bob Ellis.\r\nIn the small town of Castle Rock, the setting of many of King's most iconic stories, Scott is engaged in a low grade--but escalating--battle with the lesbians next door whose dog regularly drops his business on Scott's lawn. One of the women is friendly; the other, cold as ice. Both are trying to launch a new restaurant, but the people of Castle Rock want no part of a gay married couple, and the place is in trouble. When Scott finally understands the prejudices they face--including his own--he tries to help. Unlikely alliances, the annual foot race, and the mystery of Scott's affliction bring out the best in people who have indulged the worst in themselves and others.\r\n\"Written in masterly Stephen King's signature translucent...this uncharacteristically glimmering fairy tale calls unabashedly for us to rise above our differences\" (Booklist, starred review). Elevation is an antidote to our divisive culture, an \"elegant whisper of a story\" (Kirkus Reviews, starred review), \"perfect for any fan of small towns, magic, and the joys and challenges of doing the right thing\" (Publishers Weekly, starred review).", 1, 300, "Elevation" },
                    { 4, 1, 14.00m, "https://images-us.bookshop.org/ingram/9781982103521.jpg?height=500&v=v2-163db141331b144ed868ff1f8c220e1c", "#1 New York Times bestselling author Stephen King's terrifying novella about a town engulfed in a dense, mysterious mist as humanity makes its last stand against unholy destruction--originally published in the acclaimed short story collection Skeleton Crew and made into a TV series, as well as a feature film starring Thomas Jane and Marcia Gay Harden.\r\nIn the wake of a summer storm, terror descends...David Drayton, his son Billy, and their neighbor Brent Norton join dozens of others and head to the local grocery store to replenish supplies following a freak storm. Once there, they become trapped by a strange mist that has enveloped the town. As the confinement takes its toll on their nerves, a religious zealot, Mrs. Carmody, begins to play on their fears to convince them that this is God's vengeance for their sins. She insists a sacrifice must be made and two groups--those for and those against--are aligned. Clearly, staying in the store may prove fatal, and the Draytons, along with store employee Ollie Weeks, Amanda Dumfries, Irene Reppler, and Dan Miller, attempt to make their escape. But what's out there may be worse than what they left behind.\r\nThis exhilarating novella explores the horror in both the enemy you know--and the one you can only imagine.", 4, 300, "The Mist" },
                    { 5, 1, 19.99m, "https://images-us.bookshop.org/ingram/9781982110581.jpg?height=500&v=v2", "From #1 New York Times bestselling author Stephen King, the most riveting and unforgettable story of kids confronting evil since It. \"This is King at his best\" (The St. Louis Post-Dispatch).\r\nIn the middle of the night, in a house on a quiet street in suburban Minneapolis, intruders silently murder Luke Ellis's parents and load him into a black SUV. The operation takes less than two minutes. Luke will wake up at The Institute, in a room that looks just like his own, except there's no window. And outside his door are other doors, behind which are other kids with special talents--telekinesis and telepathy--who got to this place the same way Luke did: Kalisha, Nick, George, Iris, and ten-year-old Avery Dixon. They are all in Front Half. Others, Luke learns, graduated to Back Half, \"like the roach motel,\" Kalisha says. \"You check in, but you don't check out.\"\r\nIn this most sinister of institutions, the director, Mrs. Sigsby, and her staff are ruthlessly dedicated to extracting from these children the force of their extranormal gifts. There are no scruples here. If you go along, you get tokens for the vending machines. If you don't, punishment is brutal. As each new victim disappears to Back Half, Luke becomes more and more desperate to get out and get help. But no one has ever escaped from the Institute.\r\nAs psychically terrifying as Firestarter, and with the spectacular kid power of It, The Institute \"is another winner: creepy and touching and horrifyingly believable, all at once\" (The Boston Globe).", 1, 300, "The Institute" },
                    { 6, 1, 20.00m, "https://images-us.bookshop.org/ingram/9781668002193.jpg?height=500&v=v2-30fc4e19eb29e3f437069d5be51043d3", "A #1 New York Times Bestseller and New York Times Book Review Editors' Choice!\r\nLegendary storyteller Stephen King goes into the deepest well of his imagination in this spellbinding novel about a seventeen-year-old boy who inherits the keys to a parallel world where good and evil are at war, and the stakes could not be higher--for that world or ours.\r\nCharlie Reade looks like a regular high school kid, great at baseball and football, a decent student. But he carries a heavy load. His mom was killed in a hit-and-run accident when he was ten, and grief drove his dad to drink. Charlie learned how to take care of himself--and his dad. When Charlie is seventeen, he meets a dog named Radar and her aging master, Howard Bowditch, a recluse in a big house at the top of a big hill, with a locked shed in the backyard. Sometimes strange sounds emerge from it.\r\nCharlie starts doing jobs for Mr. Bowditch and loses his heart to Radar. Then, when Bowditch dies, he leaves Charlie a cassette tape telling a story no one would believe. What Bowditch knows, and has kept secret all his long life, is that inside the shed is a portal to another world.\r\nMagnificent, terrifying, and \"spellbinding...packed with glorious flights of imagination and characteristic tenderness about childhood, Fairy Tale is vintage King at his finest\" (Esquire).\r\n\"Good, evil, a kingdom to save, monsters to slay--these are the stuff that page-turners are made from.\" --Laura Miller, Slate", 6, 300, "Fairy Tale" },
                    { 7, 2, 13.99m, "https://images-us.bookshop.org/ingram/9780062857569.jpg?height=500&v=v2", "The tranquility of a luxury cruise along the Nile was shattered by the discovery that Linnet Ridgeway had been shot through the head. She was young, stylish, and beautiful. A girl who had everything . . . until she lost her life.\r\n\r\nHercule Poirot recalled an earlier outburst by a fellow passenger: \"I'd like to put my dear little pistol against her head and just press the trigger.\" Yet under the searing heat of the Egyptian sun, nothing is ever quite what it seems.\r\n\r\nA sweeping mystery of love, jealousy, and betrayal, Death on the Nile is one of Christie's most legendary and timeless works.", 2, 300, "Death on the Nile" },
                    { 8, 2, 12.49m, "https://images-us.bookshop.org/ingram/9780062073587.jpg?height=500&v=v2", "There's a serial killer on the loose, working his way through the alphabet and the whole country is in a state of panic.\r\n\r\nA is for Mrs. Ascher in Andover, B is for Betty Barnard in Bexhill, C is for Sir Carmichael Clarke in Churston. With each murder, the killer is getting more confident--but leaving a trail of deliberate clues to taunt the proud Hercule Poirot might just prove to be the first, and fatal, mistake.", 2, 300, "The ABC Murders" },
                    { 9, 2, 11.99m, "https://images-us.bookshop.org/ingram/9780063213920.jpg?height=500&v=v2-90c9ff960752d3dcc13ba0dd00000000", "Miss Marple encounters a compelling murder mystery in St. Mary Mead, where under the seemingly peaceful exterior of an English country village lurks intrigue, guilt, deception, and death.\r\n\r\nColonel Protheroe, local magistrate and overbearing landowner is the most detested man in the village. Everyone--even in the vicar--wishes he were dead. And very soon he is--shot in the head in the vicar's own study. Faced with a surfeit of suspects, only the inscrutable Miss Marple can unravel the tangled web of clues that will lead to the unmasking of the killer.", 2, 300, "Murder at the Vicarage" },
                    { 10, 2, 10.99m, "https://images-us.bookshop.org/ingram/9780525565109.jpg?height=500&v=v2-024505a003a9ce9007bd22711273b220", "A refugee of the Great War, Poirot is settling in England near Styles Court, the country estate of his wealthy benefactress, the elderly Emily Inglethorp. When Emily is poisoned and the authorities are baffled, Poirot puts his prodigious sleuthing skills to work.\r\nSuspects are plentiful, including the victim's much younger husband, her resentful stepsons, her longtime hired companion, a young family friend working as a nurse, and a London specialist on poisons who just happens to be visiting the nearby village. All of them have secrets they are desperate to keep, but none can outwit Poirot as he navigates the ingenious red herrings and plot twists that earned Agatha Christie her well-deserved reputation as the queen of mystery.", 2, 300, "The Mysterious Affair at Styles" },
                    { 11, 2, 12.99m, "https://images-us.bookshop.org/ingram/9780062073938.jpg?height=500&v=v2", "The beautiful bronzed body of Arlena Stuart lay face down on the beach. But strangely, there was no sun and Arlena was not sunbathing...she had been strangled.\r\n\r\nEver since Arlena's arrival the air had been thick with sexual tension. Each of the guests had a motive to kill her, including Arlena's new husband. But Hercule Poirot suspects that this apparent \"crime of passion\" conceals something much more evil.", 2, 300, "Evil Under the Sun" },
                    { 12, 2, 11.49m, "https://images-us.bookshop.org/ingram/9780063214019.jpg?height=500&v=v2", "It's seven in the morning. The Bantrys wake to find the body of a young woman in their library. She is wearing an evening dress and heavy makeup, which is now smeared across her cheeks. But who is she? How did she get there? And what is the connection with another dead girl, whose charred remains are later discovered in an abandoned quarry?\r\n\r\nThe respectable Bantrys invite Miss Marple into their home to investigate. Amid rumors of scandal, she baits a clever trap to catch a ruthless killer.", 2, 300, "The Body in the Library" },
                    { 13, 3, 14.99m, "https://images-us.bookshop.org/ingram/9780440221319.jpg?height=500&v=v2-94d9c7182e1869191f290de3a5e00c05", "\"On a June day, a young woman in a summer dress steps off a Chicago-bound bus into a small midwestern town. She doesn't intend to stay. She is just passing through. Yet her stopping here has a reason and it is part of a story that you will never forget.\"\r\nThe time is the 1950s, when life was simpler, people still believed in dreams, and family was, very nearly, everything. The place is a small midwestern town with a high school and a downtown, a skating pond and a movie house. And on a tree-lined street in the heartland of America, an extraordinary set of events begins to unfold. And gradually what seems serendipitous is tinged with purpose. A happy home is shattered by a child's senseless death. A loving marriage starts to unravel. And a stranger arrives--a young woman who will\r\ntouch many lives before she moves on. She and a young man will meet and fall in love. Their love, so innocent and full of hope, helps to restore a family's dreams. And all of their lives will be changed forever by the precious gift she leaves them.\r\n\"The Gift,\" Danielle Steel's thirty-third best-selling work, is a magical story told with stunning simplicity and power. It reveals a relationship so moving it will take your breath away. And it tells a haunting and beautiful truth about the unpredictability--and the wonder--of life.", 3, 300, "The Gift" },
                    { 14, 3, 13.49m, "https://images-us.bookshop.org/ingram/9780425285411.jpg?height=500&v=v2-c435dee7ff2667addb522dd3e6209ed3", "Angélique Latham has grown up at magnificent Belgrave Castle under the loving tutelage of her father, the Duke of Westerfield, after the death of her aristocratic French mother. At eighteen she is her father's closest, most trusted child, schooled in managing their grand estate. But when he dies, her half-brothers brutally turn her out, denying her very existence. Angélique has a keen mind, remarkable beauty, and an envelope of money her father pressed upon her. To survive, she will need all her resources--and one bold stroke of fortune.\r\nUnable to secure employment without references or connections, Angélique desperately makes her way to Paris, where she rescues a young woman fleeing an abusive madam--and suddenly sees a possibility: Open an elegant house of pleasure that will protect its women and serve only the best clients. With her upper-class breeding, her impeccable style, and her father's bequest, Angélique creates Le Boudoir, soon a sensational establishment where powerful men, secret desires, and beautiful, sophisticated women come together. But living on the edge of scandal, can she ever make a life of her own--or regain her rightful place in the world?\r\nFrom England to Paris to New York, Danielle Steel captures an age of upheaval and the struggles of women in a male-ruled society--and paints a captivating portrait of a woman of unquenchable spirit, who in houses great or humble is every ounce a duchess.", 3, 300, "The Duchess" },
                    { 15, 3, 12.99m, "https://images-us.bookshop.org/ingram/9780440236757.jpg?height=500&v=v2-cb42a06bf412ac334d9fd83e08c2b2c1", "In her 55th bestselling novel, Danielle Steel explores the seasons of an extraordinary friendship, weaving the story of three couples, lifelong friends, for whom a month's holiday in St. Tropez becomes a summer of change, revelation, secrets, surprises, and new beginnings . . .\r\nAs Diana Morrison laid the table for six at her elegant Central Park apartment, there was no warning of what was to come. Spending New Year's Eve together was a sacred tradition for Diana, her husband of thirty-two years, Eric, and their best friends, Pascale and John Donnally and Anne and Robert Smith. The future looked rosy as the long-time friends sipped champagne and talked of renting a villa together in the South of France the following summer. But life had other plans . . .", 3, 300, "Sunset in St. Tropez" },
                    { 16, 3, 11.99m, "https://images-us.bookshop.org/ingram/9780399179617.jpg?height=500&v=v2-048aef1c67c4ba7e47174acb7a525137", "From the glamorous San Francisco social scene of the 1920s, through war and the social changes of the '60s, to the rise of Silicon Valley today, this extraordinary novel takes us on a family odyssey that is both heartbreaking and inspiring, as each generation faces the challenges of their day.\r\nThe Parisian design houses in 1928, the crash of 1929, the losses of war, the drug culture of the 1960s--history holds many surprises, and lives are changed forever. For richer or for poorer, in cramped apartments and grand mansions, the treasured wedding dress made in Paris in 1928 follows each generation into their new lives, and represents different hopes for each of them, as they marry very different men.\r\nFrom inherited fortunes at the outset to self-made men and women, the wedding dress remains a cherished constant for the women who wear it in each generation and forge a destiny of their own. It is a symbol of their remaining traditions and the bond of family they share in an ever-changing world.", 3, 300, "The Wedding Dress" },
                    { 17, 3, 13.99m, "https://images-us.bookshop.org/ingram/9780440211891.jpg?height=500&v=v2-7922cbe80a9a17c0b8d483c0360088b2", "Bill Thigpen, writer producer of the No.1 daytime TV drama was so busy watching his career soar that he never noticed his marriage collapse. Now, nine years later, living alone in Hollywood, even without his wife and kids, his life and success are still reasonably sweet. Top-of-the-chart ratings, good-natured casual affairs, and special vacations with his two young sons. His life is in perfect balance, he thinks.\r\nAdrian Townshed thought she had everything: a job she liked as a TV production assistant and a handsome husband who was a rising star in his own field. In as enviable life they'd worked hard for -- the American Dream. Until she got pregnant. Suddenly all she had was chaos. And Steven's ultimatum. Him or the baby. The question was: did he mean it? He did.\r\n\r\nBill Thigpen and Adrian Townshed collided in a supermarket. And the very sight of her suddenly makes him want more in his life.... a woman he really loves, a real family again. But does he need the heartache of another man's baby, another wife? Neither does. But they couldn't help it.\r\n\r\nDanielle Steel touches the \"Heartbeat\" of two wonderful people as their friendship deepens into love, as they meet the obstacles that life presents with humor, humanity, and courage.", 3, 300, "Heartbeat" },
                    { 18, 3, 12.49m, "https://images-us.bookshop.org/ingram/9780440245193.jpg?height=500&v=v2-2a048a387e89e9ec0a491ca57b23b6c2", "Annie Ferguson was a bright young Manhattan architect with a limitless future--until a single phone call changed the course of her life forever. Overnight, she became the mother to her sister's three orphaned children, keeping a promise she never regretted making, even if it meant putting her own life indefinitely on hold.\r\nNow, at forty-two, still happily single with a satisfying career and a family that means everything to her, Annie is suddenly facing an empty nest. With her nephew and nieces now grown and confronting challenges of their own, she must navigate a parent's difficult passage between helping and letting go. The eldest, twenty-eight-year-old Liz, an overworked editor in a high-powered job at Vogue, has never allowed any man to come close enough to hurt her. Ted, at twenty-four a serious law student, is captivated by a much older woman with children, who is leading him much further than he wants to go. And the impulsive youngest, twenty-one-year old Katie, is an art student about to make a choice that will lead her to a world she is in no way prepared for but determined to embrace.\r\nThen, when least expected, a chance encounter changes Annie's life again in the most surprising direction of all. . . .", 3, 300, "Family Ties" },
                    { 19, 4, 14.99m, "https://images-us.bookshop.org/ingram/9780451457998.jpg?height=500&v=v2", "From the savannas of Africa at the dawn of mankind to the rings of Saturn as man ventures to the outer rim of our solar system, 2001: A Space Odyssey is a journey unlike any other.\r\nThis allegory about humanity's exploration of the universe--and the universe's reaction to humanity--is a hallmark achievement in storytelling that follows the crew of the spacecraft Discovery as they embark on a mission to Saturn. Their vessel is controlled by HAL 9000, an artificially intelligent supercomputer capable of the highest level of cognitive functioning that rivals--and perhaps threatens--the human mind.\r\nGrappling with space exploration, the perils of technology, and the limits of human power, 2001: A Space Odyssey continues to be an enduring classic of cinematic scope.", 4, 300, "2001: A Space Odyssey" },
                    { 20, 4, 13.49m, "https://images-us.bookshop.org/ingram/9780358380221.jpg?height=500&v=v2", "An enormous cylindrical object has entered Earth's solar system on a collision course with the sun. A team of astronauts are sent to explore the mysterious craft, which the denizens of the solar system name Rama. What they find is astonishing evidence of a civilization far more advanced than ours. They find an interior stretching over fifty kilometers; a forbidding cylindrical sea; mysterious and inaccessible buildings; and strange machine-animal hybrids, or \"biots,\" that inhabit the ship. But what they don't find is an alien presence. So who--and where--are the Ramans?", 4, 300, "Rendezvous with Rama" },
                    { 21, 4, 12.99m, "https://images-us.bookshop.org/ingram/9780345347954.jpg?height=500&v=v2-a7c1aa103cfc927e19232d71d64c37ff", "The Overlords appeared suddenly over every city--intellectually, technologically, and militarily superior to humankind. Benevolent, they made few demands: unify earth, eliminate poverty, and end war. With little rebellion, humankind agreed, and a golden age began.\r\n\r\nBut at what cost? With the advent of peace, man ceases to strive for creative greatness, and a malaise settles over the human race. To those who resist, it becomes evident that the Overlords have an agenda of their own.\r\n\r\nAs civilization approaches the crossroads, will the Overlords spell the end for humankind...or the beginning?", 4, 300, "Childhood's End" },
                    { 22, 4, 11.99m, "https://images-us.bookshop.org/ingram/9780795300080.jpg?height=500&v=v2-270a9ecae93b0a8a9e64406e0f889a8b", "Renowned structural engineer Dr. Vannevar Morgan seeks to link Earth to the stars by constructing a space elevator that will connect to an orbiting satellite 22,300 miles from the planet's surface. The elevator would lift interstellar spaceships into orbit without the need of rockets to blast through the Earth's atmosphere--making space travel easier and more cost-effective.\r\nUnfortunately, the only appropriate surface base for the elevator is located at the top of a mountain already occupied by an ancient order of Buddhist monks who strongly oppose the project. Morgan must face down their opposition--as well as enormous technical, political, and economic challenges--if he is to create his beanstalk to the heavens.\r\nAn epic novel of daring dreams spanning twenty decades, this award-winning drama combines believable science with heart-stopping suspense.", 4, 300, "The Fountains of Paradise" },
                    { 23, 4, 13.99m, "https://images-us.bookshop.org/ingram/9780795300073.jpg?height=500&v=v2-705070e921ccae784d7d690f22665abc", "Far in the future, Earth's oceans have evaporated and humanity has all but vanished. The inhabitants of Diaspar believe their domed city is all that remains of an empire that had once conquered the stars. Inside the dome, the citizens live in technological splendor, free from the distractions of aging and disease. Everything is controlled precisely, just as the city's designers had intended.\r\nBut a boy named Alvin, unlike his fellow humans, shows an insatiable--and dangerous--curiosity about the world outside the dome. His questions will send him on a quest to discover the truth about the city and humanity's history--as well as its future.\r\nA masterful and awe-inspiring work of imagination, The City and the Stars is considered one of Arthur C. Clarke's finest novels.", 4, 300, "The City and the Stars" },
                    { 24, 4, 12.49m, "https://images-us.bookshop.org/ingram/9780795300097.jpg?height=500&v=v2-2fb8cd9d565e2a2f0f6f722228a11646", "First published in 1951, before the achievement of space flight, Arthur C. Clarke created this visionary tale. Renowned science fiction writer Martin Gibson joins the spaceship Ares, the world's first interplanetary ship for passenger travel, on its maiden voyage to Mars. His mission: to report back to the home planet about the new Mars colony and the progress it has been making.\r\nIn The Sands of Mars, Clarke addresses hard physical and scientific issues with aplomb--and the best scientific understanding of the times. Included are the challenges of differing air pressures, lack of oxygen, food provisions, severe weather patterns, construction on Mars, and methods of local travel--both on the surface and to the planet's two moons.", 4, 300, "The Sands of Mars" },
                    { 25, 5, 14.99m, "https://images-us.bookshop.org/ingram/9780451417855.jpg?height=500&v=v2", "With wry humor and penetrating satire, Flatland takes us on a mind-expanding journey into a different world to give us a new vision of our own. A. Square, the slightly befuddled narrator, is born into a place limited to two dimensions--irrevocably flat--and peopled by a hierarchy of geometrical forms. In a Gulliver-like tour of his bizarre homeland, A. Square spins a fascinating tale of domestic drama and political turmoil, from sex among consenting triangles to the intentional subjugation of Flatland's females. He tells of visits to Lineland, the world of one dimension, and Pointland, the world of no dimension. But when A. Square dares to speak openly of a third, or even a fourth, dimension, his tragic fate climaxes a brilliant parody of Victorian society. An underground favorite since its publication in England in1884, Flatland is as prophetic a science fiction classic as the works of H. G. Wells, introducing aspects of relativity and hyperspace years before Einstein's famous theories. And it does so with wonderful, enduring enchantment.\r\nWith an Introduction by Valerie Smith and a New Afterword by John Allen Paulos", 6, 300, "Flatland: A Romance of Many Dimensions" },
                    { 26, 5, 13.49m, "https://images-us.bookshop.org/ingram/9780062732767.jpg?height=500&v=v2", "A century-old classic of British letters that charmed and fascinated generations of readers with its witty satire of Victorian society and its unique insights, by analogy, into the fourth dimension.", 6, 300, "Sphere Land: A Fantasy of Numerous Dimensions" },
                    { 27, 5, 12.99m, "https://images-us.bookshop.org/ingram/9781539346920.jpg?height=500&v=v2", "Edwin Abbott Abbott (1838-1926) has been ranked as one of the leading scholars and theologians of the Victorian era. He received highest honors in mathematics, classics, and theology at St. John's College, Cambridge, and in 1862 began a brilliant career, during which he served as schoolmaster of some of England's outstanding schools. At the same time he distinguished himself as a scholar, and in 1889 he retired to his studies. Although Flatland, a literary jeu d'esprit, has given pleasure to thousands of readers over many generations, Abbott is best known for his scholarly works, especially his Shakespearian Grammar and his life of Francis Bacon, and for a number of theological discussions.", 6, 300, "Planilandia" },
                    { 28, 6, 15.99m, "https://images-us.bookshop.org/ingram/9780547928227.jpg?height=500&v=v2-3638bc8450bf239b1060ea8da5a4ef69", "\"In a hole in the ground there lived a hobbit.\" So begins one of the most beloved and delightful tales in the English language--Tolkien's prelude to The Lord of the Rings. Set in the imaginary world of Middle-earth, at once a classic myth and a modern fairy tale, The Hobbit is one of literature's most enduring and well-loved novels.\r\n\r\nBilbo Baggins is a hobbit who enjoys a comfortable, unambitious life, rarely traveling any farther than his pantry or cellar. But his contentment is disturbed when the wizard Gandalf and a company of dwarves arrive on his doorstep one day to whisk him away on an adventure. They have launched a plot to raid the treasure hoard guarded by Smaug the Magnificent, a large and very dangerous dragon. Bilbo reluctantly joins their quest, unaware that on his journey to the Lonely Mountain he will encounter both a magic ring and a frightening creature known as Gollum.", 6, 300, "The Hobbit" },
                    { 29, 6, 18.99m, "https://images-us.bookshop.org/ingram/9780593500484.jpg?height=500&v=v2-3c00bad19025e703fc6ed053b761100b", "The dark, fearsome Ringwraiths are searching for a Hobbit. Frodo Baggins knows that they are seeking him and the Ring he bears--the Ring of Power that will enable evil Sauron to destroy all that is good in Middle-earth. Now it is up to Frodo and his faithful servant, Sam, with a small band of companions, to carry the Ring to the one place it can be destroyed: Mount Doom, in the very center of Sauron's realm.", 6, 300, "The Lord of the Rings: The Fellowship of the Ring" },
                    { 30, 6, 19.99m, "https://images-us.bookshop.org/ingram/9780593500491.jpg?height=500&v=v2-3bb31fa53ce3c53e27f58790bdbcdccf", "The Fellowship is scattered. Some brace hopelessly for war against the ancient evil of Sauron. Others must contend with the treachery of the wizard Saruman. Only Frodo and Sam are left to take the One Ring, ruler of the accursed Rings of Power, to be destroyed in Mordor, the dark realm where Sauron is supreme. Their guide is Gollum, deceitful and obsessive slave to the corruption of the Ring.", 6, 300, "The Lord of the Rings: The Two Towers" },
                    { 31, 6, 20.99m, "https://images-us.bookshop.org/ingram/9780593500507.jpg?height=500&v=v2-2f0b7b2c43ee945a796f0094a0f4ad6d", "While the evil might of the Dark Lord Sauron swarms out to conquer all Middle-earth, Frodo and Sam struggle deep into Mordor, seat of Sauron's power. To defeat the Dark Lord, the One Ring, ruler of the accursed Rings of Power, must be destroyed in the fires of Mount Doom. But the way is impossibly hard, and Frodo is weakening. Weighed down by the compulsion of the Ring, he begins finally to despair.", 6, 300, "The Lord of the Rings: The Return of the King" },
                    { 32, 7, 16.99m, "https://images-us.bookshop.org/ingram/9781582348254.jpg?height=500&v=v2", "In case you don't remember, Harry Potter is an eleven year old wizard. Long ago, Harry's parents were killed in a battle with the evil Lord Voldemort. When we first meet Harry, he is living miserably with his repulsive and non-magical (or Muggle) Aunt Petunia and Uncle Vernon and their even more revolting son Dudley. Following a bizarre but hilarious chain of events, Harry finds himself at Hogwarts School of Witchcraft and Wizardry, with an outrageous cast of characters, including super-smart Hermione, vile Draco Malfoy, sinister Professor Snape and the wise Headmaster Albus Dumbledore. Adventures galore ensue...", 6, 300, "Harry Potter and the Philosopher's Stone" },
                    { 33, 7, 17.99m, "https://images-us.bookshop.org/ingram/9780545791328.jpg?height=500&v=v2-92c4e6bd29453ab78d90aad2b6d8a267", "Award-winning artist Jim Kay illustrates year two of Harry Potter's adventures at Hogwarts, in a stunning, gift-ready format.\r\nThe Dursleys were so mean and hideous that summer that all Harry Potter wanted was to get back to the Hogwarts School for Witchcraft and Wizardry. But just as he's packing his bags, Harry receives a warning from a strange, impish creature named Dobby who says that if Harry Potter returns to Hogwarts, disaster will strike.And strike it does. For in Harry's second year at Hogwarts, fresh torments and horrors arise, including an outrageously stuck-up new professor, Gilderoy Lockhart, a spirit named Moaning Myrtle who haunts the girls' bathroom, and the unwanted attentions of Ron Weasley's younger sister, Ginny.But each of these seem minor annoyances when the real trouble begins, and someone -- or something -- starts turning Hogwarts students to stone. Could it be Draco Malfoy, a more poisonous rival than ever? Could it possibly be Hagrid, whose mysterious past is finally told? Or could it be the one everyone at Hogwarts most suspects... Harry Potter himself?", 6, 300, "Harry Potter and the Chamber of Secrets" },
                    { 34, 7, 18.99m, "https://images-us.bookshop.org/ingram/9780545791342.jpg?height=500&v=v2-46b4ea30d41f58f77549d93c306e1b87", "The third book in the bestselling Harry Potter series, now illustrated in glorious full color by award-winning artist Jim Kay!\r\nFor twelve long years, the dread fortress of Azkaban held an infamous prisoner named Sirius Black. Convicted of killing thirteen people with a single curse, he was said to be the heir apparent to the Dark Lord, Voldemort.Now he has escaped, leaving only two clues as to where he might be headed: Harry Potter's defeat of You-Know-Who was Black's downfall as well. And the Azkaban guards heard Black muttering in his sleep, \"He's at Hogwarts . . . he's at Hogwarts.\"Harry Potter isn't safe, not even within the walls of his magical school, surrounded by his friends. Because on top of it all, there may well be a traitor in their midst.", 6, 300, "Harry Potter and the Prisoner of Azkaban" },
                    { 35, 7, 19.99m, "https://images-us.bookshop.org/ingram/9780545791427.jpg?height=500&v=v2-06bc487df45e8029e2e0850e522c8b11", "The fourth book in the beloved Harry Potter series, now illustrated in glorious full color by award-winning artist Jim Kay. With over 150 illustrations!\r\nHarry Potter wants to get away from the pernicious Dursleys and go to the International Quidditch Cup with Hermione, Ron, and the Weasleys. He wants to dream about Cho Chang, his crush (and maybe do more than dream). He wants to find out about the mysterious event involving two other rival schools of magic, and a competition that hasn't happened for a hundred years. He wants to be a normal, fourteen-year-old wizard. Unfortunately for Harry Potter, he's not normal - even by wizarding standards. And in this case, different can be deadly.With over 150 dazzling illustrations from Jim Kay, this new fully illustrated edition of the complete and unabridged text of Harry Potter and the Goblet of Fire is sure to delight fans and first-time readers alike.", 6, 300, "Harry Potter and the Goblet of Fire" },
                    { 36, 7, 20.99m, "https://images-us.bookshop.org/ingram/9780545791434.jpg?height=500&v=v2-64db2dd638bec67593206bdbb41994e8", "There is a door at the end of a silent corridor. And it's haunting Harry Potter's dreams. Why else would he be waking in the middle of the night, screaming in terror? It's not just the upcoming O.W.L. exams; a new teacher with a personality like poisoned honey; a venomous, disgruntled house-elf; or even the growing threat of He-Who-Must-Not-Be-Named. Now Harry Potter is faced with the unreliability of the very government of the magical world and the impotence of the authorities at Hogwarts. Despite this (or perhaps because of it), he finds depth and strength in his friends, beyond what even he knew; boundless loyalty; and unbearable sacrifice.\r\n\r\nThis stunning illustrated edition brings together the talents of award-winning artists Jim Kay and Neil Packer in a visual feast, featuring iconic scenes and much loved characters -- Tonks, Luna Lovegood, and many more -- as the Order of the Phoenix keeps watch over Harry Potter's fifth year at Hogwarts. With its oversized format, high-quality paper, ribbon bookmark, and color on nearly every page, this edition is the perfect gift for Harry Potter fans and book lovers of all ages.", 6, 300, "Harry Potter and the Order of the Phoenix" },
                    { 37, 7, 21.99m, "https://images-us.bookshop.org/ingram/9781594132216.jpg?height=500&v=v2", "Book 6 in the Harry Potter seriesA New York Times BestsellerIn the fifth and most recent book, Harry Potter and the Order of the Phoenix, the last chapter, titled The Second War Begins, started: 'In a brief statement Friday night, Minister of Magic Cornelius Fudge confirmed that He-Who-Must-Not-Be-Named has returned to this country and is active once more. It is with great regret that I must confirm that the wizard styling himself Lord -- well, you know who I mean -- is alive among us again, said Fudge.'Harry Potter and the Half-Blood Prince takes up the story of Harry Potter's sixth year at Hogwarts School of Witchcraft and Wizardry at this point in the midst of the storm of this battle of good and evil. The author has already said that the Half-Blood Prince is neither Harry nor Voldemort. And most importantly, the opening chapter of Harry Potter and the Half-Blood Prince has been brewing in J. K. Rowling's mind for 13 years.", 6, 300, "Harry Potter and the Half-Blood Prince" },
                    { 38, 7, 22.99m, "https://images-us.bookshop.org/ingram/9781338878981.jpg?height=500&v=v2-e4f67a17c40b2542171ef988ab8748af", "Follow Harry, Ron, and Hermione as they embark on a dangerous quest to find and destroy Voldemort's remaining Horcruxes.", 6, 300, "Harry Potter and the Deathly Hallows" }
                });

            migrationBuilder.InsertData(
                table: "GenrePromotions",
                columns: new[] { "GenreId", "PromotionId" },
                values: new object[] { 1, 1 });

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

            migrationBuilder.CreateIndex(
                name: "IX_PlacedOrders_UserId",
                table: "PlacedOrders",
                column: "UserId");
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
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "PlacedOrders");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
