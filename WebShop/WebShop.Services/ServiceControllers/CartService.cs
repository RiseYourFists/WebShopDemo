﻿namespace WebShop.Services.ServiceControllers
{
    using Microsoft.EntityFrameworkCore;

    using WebShop.Core.Models.BookShop;
    using Models.BookShop;
    using WebShop.Core.Contracts;

    public class CartService
    {
        private readonly IBookShopRepository _repository;

        public CartService(IBookShopRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CartListItem>> GetShopItems(Dictionary<int, int> items)
        {
            var books = await _repository
                .All<Book>()
                .Where(b => items.Keys.Contains(b.Id))
                .ToListAsync();

            var result = books
                .Select(b => new CartListItem()
                {
                    Id = b.Id,
                    Title = b.Title,
                    CoverPhoto = b.BookCover,
                    TotalPrice = (b.BasePrice * (1 - (GetPromotion(_repository, b.GenreId, b.AuthorId).Result / 100))) * items[b.Id],
                    Quantity = items[b.Id]
                })
                .ToList();

            return result;

        }

        private async Task<decimal> GetPromotion(IRepository repository, int genreId, int authorId)
        {
            var promotion = await repository
                .AllReadonly<Promotion>()
                .Include(p => p.AuthorPromotions)
                .Include(p => p.GenrePromotions)
                .Where(p =>
                    (p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now) &&
                    (p.GenrePromotions
                         .Any(gp => gp.GenreId == genreId) ||
                     p.AuthorPromotions
                         .Any(ap => ap.AuthorId == authorId))
                )
                .Select(p => (decimal?)p.DiscountPercent)
                .FirstOrDefaultAsync();
            return promotion != null ? promotion.Value : 0;
        }
    }
}
