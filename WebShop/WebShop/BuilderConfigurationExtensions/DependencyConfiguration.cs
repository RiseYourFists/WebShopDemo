namespace WebShop.App.BuilderConfigurationExtensions
{
    using Ganss.Xss;
    using Core.Data;
    using Core.Contracts;
    using Core.Repository;
    using Services.ServiceControllers;
    using WebShop.Core.Models.Identity;
    using Services.Contracts;
    public static class DependencyConfiguration
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<UserHelper<ApplicationUser, Guid>>();
            services.AddScoped<IBookShopRepository, BookShopRepository>();
            services.AddScoped<IHtmlSanitizer, HtmlSanitizer>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IBookShopUtilityRepository, BookShopUtilityRepository>();

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<BookShopService>();
            services.AddScoped<CartService>();
            services.AddScoped<MyOrderService>();
            services.AddScoped<AdministrationService>();
            services.AddScoped<EmployeeService>();
            services.AddScoped<IBookShopUtilityService, BookShopUtilityService>();
            return services;
        }
    }
}
