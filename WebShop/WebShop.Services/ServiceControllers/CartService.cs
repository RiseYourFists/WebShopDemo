namespace WebShop.Services.ServiceControllers
{
    using Microsoft.EntityFrameworkCore;

    using WebShop.Core.Models.BookShop;
    using Models.BookShop;
    using WebShop.Core.Contracts;
    using static ErrorMessages.CartErrors;

    public class CartService
    {
        private readonly IBookShopRepository _repository;

        public CartService(IBookShopRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets visual information of the cart and calculates the total price of each item.
        /// </summary>
        /// <param name="items">A dictionary of Id =&gt; Quantity</param>
        /// <returns> Task&lt;List&lt;CartListItem&gt;&gt;</returns>
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

        /// <summary>
        /// Gets the total price of all books from the cart.
        /// </summary>
        /// <param name="items">A dictionary of Id =&gt; Quantity</param>
        /// <returns>Task&lt;decimal&gt;</returns>
        public async Task<decimal> GetTotalPrice(Dictionary<int, int> items)
        {
            var books = await _repository
                .All<Book>()
                .Where(b => items.Keys.Contains(b.Id))
                .ToListAsync();

            var result = books
                .Sum(r => r.BasePrice * (1 - GetPromotion(_repository, r.GenreId, r.AuthorId).Result / 100) * items[r.Id]);

            return result;
        }

        /// <summary>
        /// Checks if all items exist in the context and have enough quantity in stock.
        /// </summary>
        /// <param name="items">A dictionary of Id =&gt; Quantity</param>
        /// <returns>Task&lt;bool&gt;</returns>
        public async Task<bool> IsCartValid(Dictionary<int, int> items)
        {
            var keys = items.Keys;
            bool isValid = true;
            foreach (var key in keys)
            {
                isValid = await _repository
                    .AllReadonly<Book>()
                    .AnyAsync(b => b.Id == key && b.StockQuantity >= items[key]);

                if (!isValid)
                {
                    return isValid;
                }
            }

            return isValid;
        }

        /// <summary>
        /// Adds new placed order to the context with Pending status.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task AddNewOrder(Dictionary<int, int> items, OrderModel model, Guid userId)
        {
            if (!await IsCartValid(items))
            {
                throw new InvalidOperationException(InvalidCartInfo);
            }

            var order = new PlacedOrder()
            {
                UserId = userId,
                DatePlaced = DateTime.Now,
                IsShipped = false,
                Country = model.Country,
                City = model.City,
                Address = model.Address
            };

            var orderedItems = new List<PlacedOrderBook>();

            foreach (var (key, value) in items)
            {
                var book = await _repository.GetByIdAsync<Book>(key);
                orderedItems.Add(new()
                {
                    BookId = key,
                    Quantity = value,
                    PlacedOrder = order,
                    SingleItemPrice = book.BasePrice * (1 - (await GetPromotion(_repository, book.GenreId, book.AuthorId) / 100))
                });

                book.StockQuantity -= value;
            }

            await _repository.AddAsync(order);
            await _repository.AddRangeAsync(orderedItems);
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the last PlacedOrder made from the user.
        /// Returns null if no record is found.
        /// </summary>
        /// <param name="userId">User key identifier.</param>
        /// <returns>Task&lt;Invoice?&gt;</returns>
        public async Task<Invoice?> GetCurrentInvoice(Guid userId)
        {
            var result = await _repository
                .AllReadonly<PlacedOrder>()
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.DatePlaced)
                .Take(1)
                .Select(o => new Invoice()
                {
                    CustomerName = $"{o.User.FirstName} {o.User.LastName}",
                    PhoneNumber = o.User.PhoneNumber,
                    Country = o.Country,
                    City = o.City,
                    Address = o.Address,
                    TotalPrice = o.PlacedOrderBooks
                        .Where(ob => ob.PlacedOrderId == o.Id)
                        .Sum(ob => ob.SingleItemPrice * ob.Quantity)
                })
                .FirstOrDefaultAsync();

            return result;
        }

        /// <summary>
        /// Gets the promotion discount percent if there's an existing promotion otherwise it returns 0.
        /// </summary>
        /// <param name="repository">Repository that holds the promotion.</param>
        /// <param name="genreId">Book's genre id.</param>
        /// <param name="authorId">Book's author id</param>
        /// <returns>Task&lt;decimal&gt;</returns>
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
