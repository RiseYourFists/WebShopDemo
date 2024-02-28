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

        public async Task<List<CartListItem>> GetShopItems(Dictionary<int, int> items)
        {
            return await _repository
                .All<Book>()
                .Where(b => items.Keys.Contains(b.Id))
                .Select(b => new CartListItem()
                {
                    Id = b.Id,
                    Title = b.Title,
                    CoverPhoto = b.BookCover,
                    TotalPrice = b.CurrentPrice * items[b.Id],
                    Quantity = items[b.Id]
                })
                .ToListAsync();

        }
    }
}
