﻿namespace WebShop.Services.ServiceControllers
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

        /// <summary>
        /// Gets the count of all orders that don't have a DateFulfilled.
        /// </summary>
        /// <param name="user">Current user.</param>
        /// <returns>Task&lt;int&gt;</returns>
        public async Task<int> GetActiveOrderCount(ClaimsPrincipal user)
        {
            var userId = await _userHelper.GetUserId(user);
            var result = await _repository
                .AllReadonly<PlacedOrder>()
                .CountAsync(o => o.UserId.ToString() == userId.ToString() && o.DateFulfilled.HasValue == false);

            return result;

        }

        /// <summary>
        /// Checks if there are any orders made by the user.
        /// </summary>
        /// <param name="user">Current user.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        public async Task<bool> AnyUserOrdersPresent(ClaimsPrincipal user)
        {
            var userId = await _userHelper.GetUserId(user);
            return await _repository.AllReadonly<PlacedOrder>()
                .AnyAsync(o => o.UserId == userId);
        }

        /// <summary>
        /// Gets all orders by specified filters.
        /// </summary>
        /// <param name="user">Current user.</param>
        /// <param name="status">
        ///     <para>Selects an order by its status. There are 3 possible status types:</para>
        ///     <para>  -OrderStatus.Pending: When IsShipped is false and there's no Delivery date.</para>
        ///     <para>  -OrderStatus.Shipped: When IsShipped is true and there's no delivery date.</para>
        ///     <para>  -OrderStatus.Delivered: When IsShipped is true and there is a delivery date value.</para>
        /// </param>
        /// <param name="from">Start day of search.</param>
        /// <param name="to">End day of search.</param>
        /// <param name="orderedBy">Sorting option that can sort Total Price and Order date by Asc/Desc.</param>
        /// <returns>Task&lt;List&lt;Order&gt;&gt;</returns>
        public async Task<List<Order>> GetUserOrders(ClaimsPrincipal user, OrderStatus status, OrderClause orderedBy, DateTime? from, DateTime? to)
        {
            var userId = await _userHelper.GetUserId(user);

            var orders = await _repository
                .AllReadonly<PlacedOrder>()
                .Include(o => o.PlacedOrderBooks)
                .ThenInclude(ob => ob.Book)
                .Where(o => o.UserId.ToString() == userId.ToString())
                .OrderByDescending(o => o.DatePlaced)
                .Select(o => new Order()
                {
                    Id = o.Id,
                    City = o.City,
                    Address = o.Address,
                    OrderedOn = o.DatePlaced,
                    DeliveredOn = o.DateFulfilled.HasValue 
                        ? o.DateFulfilled.Value
                        : null,
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
                        .Sum(ob => ob.SingleItemPrice * ob.Quantity),

                    OrderStatus = !o.IsShipped && !o.DateFulfilled.HasValue
                        ? OrderStatus.Pending
                        : o.IsShipped && !o.DateFulfilled.HasValue
                        ? OrderStatus.Shipped
                        : OrderStatus.Delivered
                })
                .ToListAsync();

            if (status != OrderStatus.All)
            {
                orders = orders
                    .Where(o => o.OrderStatus == status)
                    .ToList();
            }

            if (from.HasValue && to.HasValue && from.Value <= to.Value)
            {
                var fromValue = from.Value.Date;
                var toValue = to.Value.Date;

                orders = orders
                    .Where(o => o.OrderedOn.Date >= fromValue && o.OrderedOn.Date <= toValue)
                    .ToList();
            }

            orders = orderedBy switch
            {
                OrderClause.TotalPriceAsc => orders.OrderBy(o => o.TotalPrice).ToList(),
                OrderClause.TotalPriceDesc => orders.OrderByDescending(o => o.TotalPrice).ToList(),
                OrderClause.OrderDateAsc => orders.OrderBy(o => o.OrderedOn).ToList(),
                _ => orders.OrderByDescending(o => o.OrderedOn).ToList()
            };

            return orders;
        }

    }
}
