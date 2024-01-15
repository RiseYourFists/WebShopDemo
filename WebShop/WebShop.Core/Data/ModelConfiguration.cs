using Microsoft.EntityFrameworkCore;
using WebShop.Core.Models.BookShop;
using WebShop.Core.Models.Identity;

namespace WebShop.Core.Data
{
    public static class ModelConfiguration
    {
        public static ModelBuilder AddModelConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<AuthorPromotion>(builder =>
            {
                builder.HasKey(entity => new { entity.AuthorId, entity.PromotionId });
            });

            modelBuilder.Entity<GenrePromotion>(builder =>
            {
                builder.HasKey(entity => new { entity.GenreId, entity.PromotionId });
            });

            modelBuilder.Entity<PlacedOrderBook>(builder =>
            {
                builder.HasKey(entity => new { entity.BookId, entity.PlacedOrderId });
            });

            return modelBuilder;
        }
    }
}
