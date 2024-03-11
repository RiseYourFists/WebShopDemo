namespace WebShop.Services.ServiceControllers
{
    using System.Security.Claims;
    using Microsoft.EntityFrameworkCore;

    using Models.MyOrders;
    using Models.MyOrders.Enumerations;
    using WebShop.Core.Contracts;
    using WebShop.Core.Models.BookShop;
    using WebShop.Core.Models.Identity;
    public class MyOrderService
    {
        private readonly IOrdersRepository _repository;
        private readonly UserHelper<ApplicationUser, Guid> _userHelper;
        public MyOrderService(IOrdersRepository repository, UserHelper<ApplicationUser, Guid> userHelper)
        {
            _repository = repository;
            _userHelper = userHelper;
        }

        public async Task<int> GetActiveOrderCount(ClaimsPrincipal user)
        {
            var userId = await _userHelper.GetUserId(user);
            var result = await _repository
                .AllReadonly<PlacedOrder>()
                .CountAsync(o => o.UserId.ToString() == userId.ToString() && o.DateFulfilled.HasValue == false);

            return result;

        }

        public async Task<List<MyOrder>> GetUserOrders(ClaimsPrincipal user)
        {
            var userId = await _userHelper.GetUserId(user);

            var orders = await _repository
                .AllReadonly<PlacedOrder>()
                .Include(o => o.PlacedOrderBooks)
                .ThenInclude(ob => ob.Book)
                .Where(o => o.UserId.ToString() == userId.ToString())
                .OrderByDescending(o => o.DatePlaced)
                .Select(o => new MyOrder()
                {
                    Id = o.Id,
                    City = o.City,
                    Address = o.Address,
                    OrderedOn = o.DatePlaced.ToString("g"),
                    DeliveredOn = o.DateFulfilled!.Value.ToString("g"),
                    Items = o.PlacedOrderBooks
                        .Select(ob => new OrderItem()
                        {
                            Id = ob.BookId,
                            Title = ob.Book.Title,
                            Quantity = ob.Quantity,
                            Price = ob.SingleItemPrice,
                            BookCover = ob.Book.BookCover
                        })
                        .ToList(),

                    TotalPrice = o.PlacedOrderBooks
                        .Sum(ob => ob.SingleItemPrice * ob.Quantity)
                        .ToString("F"),

                    OrderStatus = !o.IsShipped && !o.DateFulfilled.HasValue
                        ? OrderStatus.Pending
                        : o.IsShipped && !o.DateFulfilled.HasValue
                        ? OrderStatus.Sending
                        : OrderStatus.Delivered
                })
                .ToListAsync();

            return orders;
        }

    }
}
