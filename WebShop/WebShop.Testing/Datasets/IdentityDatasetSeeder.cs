namespace WebShop.Testing.Datasets
{
    using Microsoft.EntityFrameworkCore;
    using Core.Models.Identity;

    public static class IdentityDatasetSeeder
    {
        public static async Task IdentityData(DbContext context)
        {
            var user1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var user2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var user3Id = Guid.Parse("33333333-3333-3333-3333-333333333333");

            var users = new List<ApplicationUser>
            {
                new()
                {
                    Id = user1Id,
                    UserName = "user1@example.com",
                    Email = "user1@example.com",
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890"
                },
                new()
                {
                    Id = user2Id,
                    UserName = "user2@example.com",
                    Email = "user2@example.com",
                    FirstName = "Jane",
                    LastName = "Smith",
                    PhoneNumber = "9876543210"
                },
                new()
                {
                    Id = user3Id,
                    UserName = "user3@example.com",
                    Email = "user3@example.com",
                    FirstName = "Alice",
                    LastName = "Johnson",
                    PhoneNumber = "5555555555"
                }
            };

            var adminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var employeeRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            var roles = new List<ApplicationRole>
            {
                new()
                {
                    Id = adminRoleId,
                    Name = "Admin"
                },
                new()
                {
                    Id = employeeRoleId,
                    Name = "Employee"
                }
            };

            var userRoles = new List<ApplicationUserRole>
            {
                new () { UserId = user1Id, RoleId = adminRoleId },
                new(){ UserId = user2Id, RoleId = employeeRoleId }
            };

            await context.AddRangeAsync(users);
            await context.AddRangeAsync(roles);
            await context.AddRangeAsync(userRoles);
            await context.SaveChangesAsync();
        }
    }
}
