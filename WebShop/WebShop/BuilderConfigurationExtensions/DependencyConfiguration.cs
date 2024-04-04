using WebShop.Services.Contracts;

namespace WebShop.App.BuilderConfigurationExtensions
{
    using Ganss.Xss;
    using Core.Data;
    using Core.Contracts;
    using Core.Repository;
    using Services.ServiceControllers;
    using WebShop.Core.Models.Identity;
    public static class DependencyConfiguration
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<BookShopService>();
            services.AddScoped<CartService>();
            services.AddScoped<MyOrderService>();
            services.AddScoped<AdministrationService>();
            services.AddScoped<EmployeeService>();

            services.AddScoped<IUserHelper<ApplicationUser, Guid>,UserHelper<ApplicationUser, Guid>>();
            services.AddScoped<IBookShopRepository, BookShopRepository>();
            services.AddScoped<IHtmlSanitizer, HtmlSanitizer>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}
