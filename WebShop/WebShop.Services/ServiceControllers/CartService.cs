namespace WebShop.Services.ServiceControllers
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

        public List<CartListItem> GetShopItems(Dictionary<int, int> items)
        {
            return _repository
                .All<Book>()
                .Where(b => items.Keys.Contains(b.Id))
                .ToList()
                .Select(b => new CartListItem()
                {
                    Id = b.Id,
                    Title = b.Title,
                    CoverPhoto = b.BookCover,
                    TotalPrice = (b.BasePrice * 1 - (GetPromotion(_repository, b.GenreId, b.AuthorId) / 100) ) * items[b.Id],
                    Quantity = items[b.Id]
                })
                .ToList();

        }

        private static  decimal GetPromotion(IRepository repository, int genreId, int authorId)
        {
            return repository
                .AllReadonly<Promotion>()
                .Include(p => p.AuthorPromotions)
                .Include(p => p.GenrePromotions)
                .Where(p =>
                    (p.StartDate >= DateTime.Now && p.EndDate <= DateTime.Now) &&
                    (p.GenrePromotions
                         .Any(gp => gp.GenreId == genreId) ||
                     p.AuthorPromotions
                         .Any(ap => ap.AuthorId == authorId))
                )
                .Select(p => (decimal?)p.DiscountPercent)
                .FirstOrDefault() ?? 0;
        }
    }
}
