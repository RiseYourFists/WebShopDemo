namespace WebShop.Testing.Datasets
{
    using Microsoft.EntityFrameworkCore;
    using Core.Models.BookShop;
    using Core.Models.Identity;

    public static class EmployeeServiceDatasetSeeder
    {
        public static async Task SeedFor_GetOrders_Tests(DbContext context)
        {
            var users = new List<ApplicationUser>()
            {
                new()
                {
                    Id = Guid.Parse("19f268d6-4e43-4d80-b040-365a65e5c0d7"),
                    FirstName = "User",
                    LastName = "One",
                    Email = "UserOne@mail.com",
                    IsActive = true
                },
                new()
                {
                    Id = Guid.Parse("a812c5f7-98c9-44f1-bb81-7d057c364e82"),
                    FirstName = "User",
                    LastName = "Two",
                    Email = "UserOne@mail.com",
                    IsActive = true
                }
            };

            var orders = new List<PlacedOrder>()
            {
                new()
                {
                    Id = Guid.Parse("3f0a7a8e-9aa5-4f18-8b11-6ec60ab1f94f"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    DateFulfilled = null,
                    IsShipped = false,
                    UserId = Guid.Parse("19f268d6-4e43-4d80-b040-365a65e5c0d7"),
                    PlacedOrderBooks = new List<PlacedOrderBook>()
                    {
                        new()
                        {
                            BookId = 1,
                            Quantity = 1,
                            SingleItemPrice = 10m,
                            PlacedOrderId = Guid.Parse("3f0a7a8e-9aa5-4f18-8b11-6ec60ab1f94f")
                        },
                        new()
                        {
                            BookId = 2,
                            Quantity = 1,
                            SingleItemPrice = 30m,
                            PlacedOrderId = Guid.Parse("3f0a7a8e-9aa5-4f18-8b11-6ec60ab1f94f")
                        }
                    }
                },
                new()
                {
                    Id = Guid.Parse("3f0a4a8e-9aa5-4f18-8b41-6ec60ab1f94a"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    DateFulfilled = null,
                    IsShipped = true,
                    UserId = Guid.Parse("19f268d6-4e43-4d80-b040-365a65e5c0d7"),
                    PlacedOrderBooks = new List<PlacedOrderBook>()
                },
                new()
                {
                    Id = Guid.Parse("3f0a7a8e-9aa5-4f18-8b41-1ec60ab1f94c"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.Parse("01-01-2011"),
                    DateFulfilled = DateTime.Parse("01-01-2012"),
                    IsShipped = true,
                    UserId = Guid.Parse("19f268d6-4e43-4d80-b040-365a65e5c0d7"),
                    PlacedOrderBooks = new List<PlacedOrderBook>()
                },
                new()
                {
                     Id = Guid.Parse("3f0a7a8e-9aa5-4f18-8b41-6ec50ab1f94b"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    DateFulfilled = null,
                    IsShipped = false,
                    UserId = Guid.Parse("a812c5f7-98c9-44f1-bb81-7d057c364e82"),
                    PlacedOrderBooks = new List<PlacedOrderBook>()
                    {
                        new()
                        {
                            BookId = 1,
                            Quantity = 1,
                            SingleItemPrice = 10m,
                            PlacedOrderId = Guid.Parse("3f0a7a8e-9aa5-4f18-8b41-6ec50ab1f94b")
                        },
                        new()
                        {
                            BookId = 2,
                            Quantity = 1,
                            SingleItemPrice = 30m,
                            PlacedOrderId = Guid.Parse("3f0a7a8e-9aa5-4f18-8b41-6ec50ab1f94b")
                        }
                    }
                }
            };

            await context.AddRangeAsync(orders);
            await context.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_GetLastPage_Tests(DbContext context)
        {
            var orders = new List<PlacedOrder>()
            {
                new()
                {
                    Id = Guid.Parse("3f0a7a8e-9aa5-4f18-8b11-6ec60ab1f94f"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    DateFulfilled = null,
                    IsShipped = false,
                    UserId = Guid.Parse("19f268d6-4e43-4d80-b040-365a65e5c0d7"),
                    PlacedOrderBooks = new List<PlacedOrderBook>()
                },
                new()
                {
                    Id = Guid.Parse("3f0a4a8e-9aa5-4f18-8b41-6ec60ab1f94a"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    DateFulfilled = null,
                    IsShipped = true,
                    UserId = Guid.Parse("19f268d6-4e43-4d80-b040-365a65e5c0d7"),
                    PlacedOrderBooks = new List<PlacedOrderBook>()
                },
                new()
                {
                    Id = Guid.Parse("3f0a7a8e-9aa5-4f18-8b41-1ec60ab1f94c"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.Parse("01-01-2011"),
                    DateFulfilled = DateTime.Parse("01-01-2012"),
                    IsShipped = true,
                    UserId = Guid.Parse("19f268d6-4e43-4d80-b040-365a65e5c0d7"),
                    PlacedOrderBooks = new List<PlacedOrderBook>()
                },
                new()
                {
                     Id = Guid.Parse("3f0a7a8e-9aa5-4f18-8b41-6ec50ab1f94b"),
                    Address = "Address",
                    City = "City",
                    Country = "Country",
                    DatePlaced = DateTime.MinValue,
                    DateFulfilled = null,
                    IsShipped = false,
                    UserId = Guid.Parse("a812c5f7-98c9-44f1-bb81-7d057c364e82"),
                    PlacedOrderBooks = new List<PlacedOrderBook>()
                }
            };

            await context.AddRangeAsync(orders);
            await context.SaveChangesAsync();
        }

        public static async Task SeedFor_MarkAsShipped_Tests(DbContext context)
        {
            await context.AddAsync(new PlacedOrder()
            {
                Id = Guid.Parse("3f0a7a8e-9aa5-4f18-8b41-6ec50ab1f94b"),
                Address = "Address",
                City = "City",
                Country = "Country",
                DatePlaced = DateTime.MinValue,
                DateFulfilled = null,
                IsShipped = false,
                UserId = Guid.NewGuid()
            });

            await context.SaveChangesAsync();
        }
        
        public static async Task SeedFor_MarkAsDelivered_Tests(DbContext context)
        {
            await context.AddAsync(new PlacedOrder()
            {
                Id = Guid.Parse("3f0a7a8e-9aa5-4f18-8b41-6ec50ab1f94b"),
                Address = "Address",
                City = "City",
                Country = "Country",
                DatePlaced = DateTime.MinValue,
                DateFulfilled = null,
                IsShipped = true,
                UserId = Guid.NewGuid()
            });

            await context.SaveChangesAsync();
        }
    }
}
