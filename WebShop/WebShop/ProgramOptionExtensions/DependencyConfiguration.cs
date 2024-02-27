using Ganss.Xss;
using WebShop.Core.Contracts;
using WebShop.Core.Data;
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

            services.AddScoped<IBookShopRepository, BookShopRepository>();
            services.AddScoped<IHtmlSanitizer, HtmlSanitizer>();
            return services;
        }
    }
}
