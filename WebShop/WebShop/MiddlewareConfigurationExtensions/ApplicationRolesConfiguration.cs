namespace WebShop.App.MiddlewareConfigurationExtensions
{
    using Microsoft.AspNetCore.Identity;
    using WebShop.Core.Models.Identity;

    public static class ApplicationRolesConfiguration
    {
        public static async Task<WebApplication> AddRolesAsync(this WebApplication app, IConfiguration configuration)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var roleManager = (RoleManager<ApplicationRole>)scope.ServiceProvider.GetService(typeof(RoleManager<ApplicationRole>));
            var roles = configuration.GetSection("ApplicationRoles").Get<List<string>>();

            if (roleManager == null)
            {
                throw new Exception("Role manager is null!");
            }

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole(role));
                }
            }
            return app;
        }
    }
}
