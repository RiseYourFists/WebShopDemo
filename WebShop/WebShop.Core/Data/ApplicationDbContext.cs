
using Microsoft.AspNetCore.Identity;
using WebShop.Core.Models.BookShop;
using WebShop.Core.Models.Identity;

namespace WebShop.Core.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<
        ApplicationUser, ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<AuthorPromotion> AuthorPromotions { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<GenrePromotion> GenrePromotions { get; set; }

        public DbSet<PlacedOrder> PlacedOrders { get; set; }

        public DbSet<PlacedOrderBook> PlacedOrderBooks { get; set; }

        public DbSet<Promotion> Promotions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddModelConfiguration();
            modelBuilder.SeedDatabase();
        }
    }
}