using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebShop.App.BuilderConfigurationExtensions;
using WebShop.Core.Data;
using WebShop.Services.ServiceControllers;

namespace WebShop.Testing
{
    public class BoolShopServiceTests
    {
        private IServiceProvider serviceProvider;

        private static readonly Assembly CurrentAssembly = typeof(BookShopService).Assembly;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            this.serviceProvider = ConfigureServices<ApplicationDbContext>(Guid.NewGuid().ToString());

        }

        private static Type GetType(string modelName)
        {
            var modelType = CurrentAssembly
                .GetTypes()
                .FirstOrDefault(t => t.Name == modelName);

            Assert.IsNotNull(modelType, $"{modelName} model not found!");

            return modelType;
        }

        private static IServiceProvider ConfigureServices<TContext>(string databaseName)
            where TContext : DbContext
        {
            var services = ConfigureDbContext<TContext>(databaseName);

            var context = services.GetService<TContext>();

            try
            {
                context.Model.GetEntityTypes();
            }
            catch (InvalidOperationException ex) when (ex.Source == "Microsoft.EntityFrameworkCore.Proxies")
            {
                services = ConfigureDbContext<TContext>(databaseName, useLazyLoading: true);
            }

            return services;
        }

        private static IServiceProvider ConfigureDbContext<TContext>(string databaseName, bool useLazyLoading = false)
            where TContext : DbContext
        {
            var services = new ServiceCollection();

            services
                .AddDbContext<TContext>(
                    options => options
                        .UseInMemoryDatabase(databaseName)
                        .UseLazyLoadingProxies(useLazyLoading)
                );

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
