namespace WebShop.Testing.Datasets
{
    using Microsoft.EntityFrameworkCore;

    using Core.Models.BookShop;

    public static class MyOrderServiceDatasets
    {
        public static async Task SeedFor_GetOrderCount_Test(DbContext context)
        {
            await context.AddRangeAsync(new List<PlacedOrder>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("3f7f6b82-527b-4e4b-bb58-ace1a0c7a281"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    IsShipped = false
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("3f7f6b82-527b-4e4b-bb58-ace1a0c7a281"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    IsShipped = true
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("3f7f6b82-527b-4e4b-bb58-ace1a0c7a281"),
                    DateFulfilled = DateTime.MaxValue,
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    IsShipped = false
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("3f7f6b82-527b-4e4b-bb58-ace1a0c7a260"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    IsShipped = false
                }
            });
            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_AnyUserOrdersPresent_TestPositive(DbContext context)
        {
            await context.AddRangeAsync(new List<PlacedOrder>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("3f7f6b82-527b-4e4b-bb58-ace1a0c7a281"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    IsShipped = false
                }
            });
            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_AnyUserOrdersPresent_TestNegative(DbContext context)
        {
            await context.AddRangeAsync(new List<PlacedOrder>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("3f7f6b82-527b-4e4b-bb58-ace1a0c7a260"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    IsShipped = false
                }
            });
            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_GetUserOrders_Test(DbContext context)
        {
            await context.AddAsync(new Book()
            {
                Id = 1,
                AuthorId = 1,
                GenreId = 1,
                Title = "Title",
                BasePrice = 10,
                BookCover = "Empty",
                Description = "Empty",
                StockQuantity = 1
            });

            await context.AddRangeAsync(new List<PlacedOrder>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("3f7f6b82-527b-4e4b-bb58-ace1a0c7a281"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    DateFulfilled = null,
                    IsShipped = false,
                    PlacedOrderBooks = new List<PlacedOrderBook>()
                    {
                        new()
                        {
                            BookId = 1,
                            Quantity = 1,
                            SingleItemPrice = 11
                        }
                    }
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("3f7f6b82-527b-4e4b-bb58-ace1a0c7a281"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    DateFulfilled = null,
                    IsShipped = true,
                    PlacedOrderBooks = new List<PlacedOrderBook>()
                    {
                        new()
                        {
                            BookId = 1,
                            Quantity = 1,
                            SingleItemPrice = 12
                        }
                    }
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("3f7f6b82-527b-4e4b-bb58-ace1a0c7a281"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    DateFulfilled = new DateTime(2014, 01, 01),
                    IsShipped = true,
                    PlacedOrderBooks = new List<PlacedOrderBook>()
                    {
                        new()
                        {
                            BookId = 1,
                            Quantity = 1,
                            SingleItemPrice = 13
                        }
                    }
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("3f7f6b82-527b-4e4b-bb58-ace1a0c7a260"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    DateFulfilled = null,
                    IsShipped = false,
                    PlacedOrderBooks = new List<PlacedOrderBook>()
                    {
                        new()
                        {
                            BookId = 1,
                            Quantity = 1,
                            SingleItemPrice = 10
                        }
                    }
                }
            });
            await context.SaveChangesAsync();
        }
    }
}
