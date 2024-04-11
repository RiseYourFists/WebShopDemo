namespace WebShop.Testing
{
    using AutoMapper;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using App.BuilderConfigurationExtensions;

    public class BaseTestSetup
    {
        public IServiceProvider serviceProvider;

        protected DbContext context;

        protected IMapper mapper;

        protected const string NullIsExpected = "Object value is expected to be null!";

        protected const string ObjectNotNull = "Object value is not supposed to be null!";

        public void Setup<TContext>() where TContext : DbContext
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            serviceProvider = ConfigureServices<TContext>(Guid.NewGuid().ToString());
            context = serviceProvider.GetService<TContext>();
            mapper = new Mapper(config);
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

        protected static string GetErrorMsg<T>(T expected, T actual)
        {
            return $"Expected: {expected}\nBut got: {actual}";
        }
    }
}
