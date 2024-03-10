using Ganss.Xss;
using WebShop.Core.Contracts;
using WebShop.Core.Data;
using WebShop.Core.Models.Identity;
using WebShop.Core.Repository;
using WebShop.Services.ServiceControllers;

namespace WebShop.App.ProgramOptionExtensions
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<BookShopService>();
            services.AddScoped<CartService>();

            services.AddScoped<UserHelper<ApplicationUser, Guid>>();
            services.AddScoped<IBookShopRepository, BookShopRepository>();
            services.AddScoped<IHtmlSanitizer, HtmlSanitizer>();
            return services;
        }
    }
}
