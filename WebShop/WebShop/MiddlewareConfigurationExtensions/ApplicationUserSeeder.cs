namespace WebShop.App.MiddlewareConfigurationExtensions
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using Core.Data;
    using WebShop.Core.Models.Identity;

    public static class ApplicationUserSeeder
    {
        public static async Task SeedUsersAsync(this IApplicationBuilder application)
        {
            await using var scope = application.ApplicationServices.CreateAsyncScope();
            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            if (context == null || userManager == null)
            {
                return;
            }

            var admin = new ApplicationUser()
            {
                Id = Guid.Parse("021EAE2A-786C-49BC-B9AF-F6EC63A7A111"),
                FirstName = "Pesho",
                LastName = "Petrov",
                IsActive = true,
                Email = "petrov_pesho@mail.com",
                PhoneNumber = "0888999912",
                UserName = "PeshoPetrov"
            };
            var employee = new ApplicationUser()
            {
                Id = Guid.Parse("B60FB78F-FCE3-485D-9F73-C5ECE8E40368"),
                FirstName = "Pepi",
                LastName = "Petrov",
                IsActive = true,
                Email = "pepi@mail.com",
                PhoneNumber = "0888999913",
                UserName = "PepiPetrov"
            };

            var doesAdminExist = await context.Users.AnyAsync(u => u.Email == admin.Email);
            var doesEmployeeExist = await context.Users.AnyAsync(u => u.Email == employee.Email);

            if (!doesAdminExist)
            {
                await userManager.CreateAsync(admin, "12345678");
            }

            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == admin.Id);
            if (user != null && !await userManager.IsInRoleAsync(user, "Admin"))
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }

            if (!doesEmployeeExist)
            {
                await userManager.CreateAsync(employee, "12345678");
            }

            user = null;
            user = await context.Users.FirstOrDefaultAsync(u => u.Id == employee.Id);
            if (user != null && !await userManager.IsInRoleAsync(user, "Employee"))
            {
                await userManager.AddToRoleAsync(user, "Employee");
            }

        }
    }
}
