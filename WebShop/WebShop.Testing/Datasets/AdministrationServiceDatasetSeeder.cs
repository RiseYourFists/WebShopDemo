namespace WebShop.Testing.Datasets
{
    using Microsoft.EntityFrameworkCore;
    using Core.Models.BookShop;
    public static class AdministrationServiceDatasetSeeder
    {
        public static async Task<int> SeedFor_GetGenres_Tests(DbContext context)
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

            return genres.Count();
        }

        public static async Task<int> SeedFor_GetAuthors_Tests(DbContext context)
        {
            var authors = new List<Author>();

            for (int i = 1; i <= 10; i++)
            {
                authors.Add(new()
                {
                    Id = i,
                    Name = $"Author{i}"
                });
            }

            await context.AddRangeAsync(authors);
            await context.SaveChangesAsync();

            return authors.Count();
        }

        public static async Task SeedFor_GetBooks_Tests(DbContext context)
        {
            var promotions = new List<Promotion>();

            var books = new List<Book>();

            promotions.Add(new()
            {
                Id = 1,
                Name = "A promotion",
                DiscountPercent = 50,
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
                AuthorPromotions = new List<AuthorPromotion>()
                {
                    new()
                    {
                        AuthorId = 1
                    }
                },
                GenrePromotions = new List<GenrePromotion>()
            });

            for (int i = 1; i <= 10; i++)
            {
                books.Add(new()
                {
                    Id = i,
                    Title = $"Title{i}",
                    AuthorId = i,
                    BasePrice = 30,
                    BookCover = "Empty",
                    Description = "Empty",
                    GenreId = i,
                    StockQuantity = 300
                });
            }

            await context.AddRangeAsync(promotions);
            await context.AddRangeAsync(books);
            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_AnyGenre_Tests(DbContext context)
        {
            await context.AddAsync(new Genre()
            {
                Id = 1,
                Name = "Genre1",
                IconLink = "Empty"
            });

            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_AnyAuthor_Tests(DbContext context)
        {
            await context.AddAsync(new Author()
            {
                Id = 1,
                Name = "Author1"
            });

            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_GetBook_Tests(DbContext context)
        {
            await context.AddAsync(new Book
            {
                Id = 1,
                Title = "Title1",
                AuthorId = 1,
                GenreId = 1,
                BasePrice = 30,
                BookCover = "Empty",
                Description = "Empty",
            });

            await context.AddAsync(new Genre
            {
                IconLink = "Empty",
                Id = 1,
                Name = "Genre1"
            });

            await context.AddAsync(new Author()
            {
                Id = 1,
                Name = "Author1"
            });

            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_EditBook_Tests(DbContext context)
        {
            await context.AddAsync(new Book
            {
                Id = 1,
                Title = "Title1",
                AuthorId = 1,
                GenreId = 1,
                BasePrice = 30,
                BookCover = "Empty",
                Description = "Empty",
            });

            await context.AddAsync(new Genre
            {
                IconLink = "Empty",
                Id = 1,
                Name = "Genre1"
            });

            await context.AddAsync(new Author()
            {
                Id = 1,
                Name = "Author1"
            });

            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_GetGenreInfo_Tests(DbContext context)
        {
            await context.AddAsync(new Genre()
            {
                Id = 1,
                Name = "Genre1",
                IconLink = "Empty"
            });

            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_GetAuthorInfo_Tests(DbContext context)
        {
            await context.AddAsync(new Author()
            {
                Id = 1,
                Name = "Author1"
            });

            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_AddNewBook_Tests(DbContext context)
        {
            await context.AddAsync(new Genre
            {
                Id = 1,
                Name = "Genre1",
                IconLink = "Empty"
            });

            await context.AddAsync(new Author
            {
                Id = 1,
                Name = "Author1"
            });

            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_GetPromotions_Tests(DbContext context)
        {
            var promotions = new List<Promotion>();

            for (int i = 1; i <= 10; i++)
            {
                promotions.Add(new()
                {
                    Id = i,
                    Name = $"Promotion{i}",
                    DiscountPercent = 50,
                    StartDate = DateTime.MinValue,
                    EndDate = DateTime.MaxValue,
                    AuthorPromotions = new List<AuthorPromotion>(),
                    GenrePromotions = new List<GenrePromotion>()
                    {
                        new()
                        {
                            GenreId = 1
                        }
                    }
                });
            }

            await context.AddRangeAsync(promotions);
            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_GetPromotion_Tests(DbContext context)
        {
            await context.AddAsync(new Promotion()
            {
                Id = 1,
                Name = "Promotion1",
                DiscountPercent = 50,
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            });

            await context.SaveChangesAsync();
        }

        public static async Task<int> SeedFor_GetPromotionAuthors_Tests(DbContext context)
        {
            var authors = new List<Author>();

            for (int i = 1; i <= 10; i++)
            {
                authors.Add(new Author()
                {
                    Id = i,
                    Name = $"Author{i}"
                });
            }

            await context.AddRangeAsync(authors);
            await context.SaveChangesAsync();
            return authors.Count;
        }

        public static async Task<int> SeedFor_GetPromotionGenres_Tests(DbContext context)
        {
            var genre = new List<Genre>();

            for (int i = 1; i <= 10; i++)
            {
                genre.Add(new Genre()
                {
                    Id = i,
                    Name = $"Genre{i}",
                    IconLink = "Empty"
                });
            }

            await context.AddRangeAsync(genre);
            await context.SaveChangesAsync();
            return genre.Count;
        }

        public static async Task SeedFor_RemovePromotions_Tests(DbContext context)
        {
            await context.AddAsync(new AuthorPromotion()
            {
                AuthorId = 1,
                PromotionId = 1
            });

            await context.AddAsync(new GenrePromotion()
            {
                GenreId = 1,
                PromotionId = 1
            });

            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_AddAndEditPromotion_Tests(DbContext context)
        {
            var promotions = new List<Promotion>()
            {
                new()
                {
                    Id = 1,
                    Name = "Promotion1",
                    DiscountPercent = 50,
                    StartDate = DateTime.MinValue,
                    EndDate = DateTime.MaxValue,
                    AuthorPromotions = new List<AuthorPromotion>(),
                    GenrePromotions = new List<GenrePromotion>()
                },
                new()
                {
                    Id = 2,
                    Name = "Promotion2",
                    DiscountPercent = 50,
                    StartDate = DateTime.MinValue,
                    EndDate = DateTime.MaxValue,
                    GenrePromotions = new List<GenrePromotion>(),
                    AuthorPromotions = new List<AuthorPromotion>()
                }
            };

            var genrePromotions = new List<GenrePromotion>()
            {
                new()
                {
                    PromotionId = 1,
                    GenreId = 1
                }
            };

            var authorPromotions = new List<AuthorPromotion>()
            {
                new()
                {
                    PromotionId = 2,
                    AuthorId = 1
                }
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
                },
                new()
                {
                    Id = 3,
                    Name = "Author3"
                }
            };

            var genres = new List<Genre>()
            {
                new()
                {
                    Id = 1,
                    Name = "Genre1",
                    IconLink = "Empty",
                },
                new()
                {
                    Id = 2,
                    Name = "Genre2",
                    IconLink = "Empty"
                },
                new()
                {
                    Id = 3,
                    Name = "Genre3",
                    IconLink = "Empty"
                }
            };

            var books = new List<Book>()
            {
                new()
                {
                    Id = 1,
                    Title = "Title1",
                    AuthorId = 1,
                    GenreId = 2,
                    BasePrice = 30,
                    BookCover = "Empty",
                    Description = "Empty",
                },
                new()
                {
                    Id = 2,
                    Title = "Title2",
                    AuthorId = 1,
                    GenreId = 1,
                    BasePrice = 30,
                    BookCover = "Empty",
                    Description = "Empty",
                },
                new()
                {
                    Id = 3,
                    Title = "Title3",
                    AuthorId = 3,
                    GenreId = 3,
                    BasePrice = 30,
                    BookCover = "Empty",
                    Description = "Empty",
                }
            };

            await context.AddRangeAsync(genres);
            await context.AddRangeAsync(authors);
            await context.AddRangeAsync(promotions);
            await context.AddRangeAsync(authorPromotions);
            await context.AddRangeAsync(genrePromotions);
            await context.AddRangeAsync(books);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
        }
    }
}
