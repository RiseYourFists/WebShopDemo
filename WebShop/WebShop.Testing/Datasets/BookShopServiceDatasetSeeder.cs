using Microsoft.EntityFrameworkCore;
using WebShop.Core.Models.BookShop;

namespace WebShop.Testing.Datasets
{
    public static class BookShopServiceDatasetSeeder
    {
        public static async Task SeedFor_TopFive_Test(DbContext context)
        {
            await context.AddRangeAsync(new List<Author>()
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
            });

            await context.AddRangeAsync(new List<Genre>()
            {
                new()
                {
                    Id = 1,
                    Name = "Genre1",
                    IconLink = "No icon"
                },
                new()
                {
                    Id = 2,
                    Name = "Genre2",
                    IconLink = "No icon"
                }
            });

            var promotion = new Promotion()
            {
                Name = "Promotion of author 2",
                DiscountPercent = 50,
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue
            };

            promotion.AuthorPromotions.Add(new AuthorPromotion()
            {
                AuthorId = 2
            });

            await context.AddAsync(promotion);

            await context.AddRangeAsync(new List<Book>()
            {
                new ()
                {
                    Id = 1,
                    Title = "Book1",
                    AuthorId = 1,
                    BookCover = "Empty",
                    Description = "Some description",
                    BasePrice = 16.99m,
                    GenreId = 1,
                    StockQuantity = 300
                },
                new()
                {
                    Id = 2,
                    Title = "Book2",
                    AuthorId = 1,
                    BookCover = "Empty",
                    Description = "Some description",
                    BasePrice = 16.99m,
                    GenreId = 1,
                    StockQuantity = 300
                },
                new()
                {
                    Id = 3,
                    Title = "Book3",
                    AuthorId = 1,
                    BookCover = "Empty",
                    Description = "Some description",
                    BasePrice = 16.99m,
                    GenreId = 1,
                    StockQuantity = 300
                },
                new()
                {
                    Id = 4,
                    Title = "Book4",
                    AuthorId = 1,
                    BookCover = "Empty",
                    Description = "Some description",
                    BasePrice = 16.99m,
                    GenreId = 1,
                    StockQuantity = 300
                },
                new()
                {
                    Id = 5,
                    Title = "Book5",
                    AuthorId = 1,
                    BookCover = "Empty",
                    Description = "Some description",
                    BasePrice = 17.99m,
                    GenreId = 1,
                    StockQuantity = 300
                },
                new()
                {
                    Id = 6,
                    Title = "Book6",
                    AuthorId = 2,
                    BookCover = "Empty",
                    Description = "Some description",
                    BasePrice = 30.00m,
                    GenreId = 1,
                    StockQuantity = 300
                }
            });

            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_GenreIcons_Test(DbContext context)
        {
            await context.AddRangeAsync(new List<Genre>()
            {
                new()
                {
                    Name = "Genre1",
                    IconLink = "Empty"
                },
                new()
                {
                    Name = "Genre2",
                    IconLink = "Empty"
                }
            });

            await context.SaveChangesAsync();
        }

        public static async Task<int> SeedFor_GetCategoryList_Tests(DbContext context)
        {
            var genres = new List<Genre>();

            for (int i = 1; i <= 10; i++)
            {
                genres.Add(new()
                {
                    Id = i,
                    Name = $"Genre{i}",
                    IconLink = "Empty"
                });
            }

            await context.AddRangeAsync(genres);
            await context.SaveChangesAsync();
            return genres.Count;
        }

        public static async Task SeedFor_GetCatalogue_Test(DbContext context)
        {
            await context.AddRangeAsync(new List<Author>()
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
            });

            await context.AddRangeAsync(new List<Book>()
            {
                new()
                {
                    Id = 1,
                    AuthorId = 1,
                    GenreId = 1,
                    Title = "Book 1",
                    Description = "Description of Book 1",
                    BookCover = "Empty",
                    BasePrice = 30.00m,
                    StockQuantity = 300
                },
                new()
                {
                    Id = 2,
                    AuthorId = 2,
                    GenreId = 2,
                    Title = "Book 2",
                    Description = "Description of Book 2",
                    BookCover = "Empty",
                    BasePrice = 25.00m,
                    StockQuantity = 200
                }
            });

            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_MaxPages_Test(DbContext context)
        {
            var books = new List<Book>();

            for (int i = 1; i <= 20; i++)
            {
                books.Add(new()
                {
                    Title = $"Book{i}",
                    GenreId = 1,
                    AuthorId = 1,
                    BookCover = "Empty",
                    Description = "Empty",
                    BasePrice = 15.00m
                });
            }

            await context.AddRangeAsync(books);
            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_GetBookInfo_Test(DbContext context)
        {
            var books = new List<Book>();
            var promotion = new Promotion()
            {
                Name = "A promotion",
                DiscountPercent = 50,
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };

            var author = new Author()
            {
                Id = 1,
                Name = "Author1"
            };

            var genres = new List<Genre>()
            {
                new()
                {
                    Id = 1,
                    Name = "Genre1",
                    IconLink = "Empty"
                },
                new()
                {
                    Id = 2,
                    Name = "Genre2",
                    IconLink = "Empty"
                }
            };

            promotion.GenrePromotions.Add(new ()
            {
                GenreId = 2
            });

            for (int i = 1; i <= 20; i++)
            {
                books.Add(new()
                {
                    Id = i,
                    Title = $"Book{i}",
                    GenreId = 1,
                    AuthorId = 1,
                    BookCover = "Empty",
                    Description = "Empty",
                    StockQuantity = 300,
                    BasePrice = 30.00m
                });
            }

            books[^1].GenreId = 2;

            await context.AddRangeAsync(genres);
            await context.AddAsync(author);
            await context.AddAsync(promotion);
            await context.AddRangeAsync(books);
            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_AnyBook_Test(DbContext context)
        {
            var books = new List<Book>();

            for (int i = 1; i <= 10; i++)
            {
                books.Add(new()
                {
                    Title = $"Book{i}",
                    GenreId = 1,
                    AuthorId = 1,
                    BookCover = "Empty",
                    Description = "Empty",
                    BasePrice = 15.00m
                });
            }

            await context.AddRangeAsync(books);
            await context.SaveChangesAsync();
        }
    }
}
