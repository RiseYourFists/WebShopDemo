
namespace WebShop.Testing.Datasets
{
    using Microsoft.EntityFrameworkCore;

    using Core.Models.Identity;
    using Core.Models.BookShop;

    public static class CartServiceDatasetSeeder
    {
        public static async Task SeedFor_GetShopItems_Test(DbContext context)
        {
            var promotion = new Promotion()
            {
                Id = 1,
                Name = "Promotion",
                DiscountPercent = 50,
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };

            var authors = new List<Author>()
            {
                new()
                {
                    Id = 1,
                    Name = "Author1"
                },
                new()
                {
                    Id = 2,
                    Name = "Author2"
                }
            };

            promotion.AuthorPromotions.Add(new()
            {
                AuthorId = 1,
                PromotionId = 1
            });

            var books = new List<Book>()
            {
                new()
                {
                    Id = 1,
                    Title = "Book1",
                    AuthorId = 1,
                    BookCover = "Empty",
                    BasePrice = 50m,
                    Description = "Empty",
                    GenreId = 1,
                    StockQuantity = 300,
                },
                new() {
                    Id = 2,
                    Title = "Book2",
                    AuthorId = 2,
                    BookCover = "Empty",
                    BasePrice = 50m,
                    Description = "Empty",
                    GenreId = 2,
                    StockQuantity = 300,
                },
                new() {
                    Id = 3,
                    Title = "Book3",
                    AuthorId = 2,
                    BookCover = "Empty",
                    BasePrice = 50m,
                    Description = "Empty",
                    GenreId = 2,
                    StockQuantity = 300,
                }
            };

            await context.AddRangeAsync(authors);
            await context.AddAsync(promotion);
            await context.AddRangeAsync(books);
            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_AddNewOrder_Test(DbContext context)
        {
            var user = new ApplicationUser()
            {
                Id = Guid.Parse("07fbc9e3-0d5f-4c5d-a1f7-ef1fd67f33c8"),
                UserName = "User",
                Email = "Email@main.com",
                FirstName = "FirstName",
                LastName = "LastName",
                IsActive = true
            };
            var promotion = new Promotion()
            {
                Id = 1,
                Name = "Promotion",
                DiscountPercent = 50,
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };

            promotion.AuthorPromotions.Add(new()
            {
                AuthorId = 1,
                PromotionId = 1
            });

            var books = new List<Book>()
            {
                new()
                {
                    Id = 1,
                    Title = "Book1",
                    AuthorId = 1,
                    BookCover = "Empty",
                    BasePrice = 50m,
                    Description = "Empty",
                    GenreId = 1,
                    StockQuantity = 300,
                },
                new() {
                    Id = 2,
                    Title = "Book2",
                    AuthorId = 2,
                    BookCover = "Empty",
                    BasePrice = 50m,
                    Description = "Empty",
                    GenreId = 2,
                    StockQuantity = 300,
                },
                new() {
                    Id = 3,
                    Title = "Book3",
                    AuthorId = 2,
                    BookCover = "Empty",
                    BasePrice = 50m,
                    Description = "Empty",
                    GenreId = 2,
                    StockQuantity = 300,
                }
            };

            await context.AddAsync(user);
            await context.AddAsync(promotion);
            await context.AddRangeAsync(books);
            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_GetInvoice_Test(DbContext context)
        {
            var user = new ApplicationUser()
            {
                Id = Guid.Parse("07fbc9e3-0d5f-4c5d-a1f7-ef1fd67f33c8"),
                UserName = "User",
                Email = "Email@main.com",
                FirstName = "FirstName",
                LastName = "LastName",
                IsActive = true
            };

            var order = new PlacedOrder()
            {
                Address = "Address",
                City = "City",
                Country = "Country",
                DateFulfilled = null,
                DatePlaced = DateTime.Now,
                Id = Guid.Parse("9d14e540-ae61-4a92-bb0e-4f8cf7d4186e"),
                IsShipped = false,
                UserId = Guid.Parse("07fbc9e3-0d5f-4c5d-a1f7-ef1fd67f33c8"),
                PlacedOrderBooks = new List<PlacedOrderBook>()
                {
                    new()
                    {
                        BookId = 1,
                        Quantity = 2,
                        SingleItemPrice = 10.00m
                    }
                }
            };

            await context.AddAsync(order);
            await context.AddAsync(user);
            await context.SaveChangesAsync();
        }
    }
}
