﻿namespace WebShop.Services.ServiceControllers
{
    using Microsoft.EntityFrameworkCore;

    using Models.MyOrders;
    using Models.MyOrders.Enumerations;
    using WebShop.Core.Contracts;
    using WebShop.Core.Models.BookShop;
    using static ErrorMessages.EmployeeErrors;

    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all orders by specified filters.
        /// </summary>
        /// <param name="status">
        ///     <para>Selects an order by its status. There are 3 possible status types:</para>
        ///     <para>  -OrderStatus.Pending: When IsShipped is false and there's no Delivery date.</para>
        ///     <para>  -OrderStatus.Shipped: When IsShipped is true and there's no delivery date.</para>
        ///     <para>  -OrderStatus.Delivered: When IsShipped is true and there is a delivery date value.</para>
        /// </param>
        /// <param name="from">Start day of search.</param>
        /// <param name="to">End day of search.</param>
        /// <param name="itemsOnPage">Determines how many items to show in page.</param>
        /// <param name="currentPage">Page section.</param>
        /// <param name="orderedBy">Sorting option that can sort Total Price and Order date by Asc/Desc.</param>
        /// <returns>Task&lt;List&lt;Order&gt;&gt;</returns>
        public async Task<List<Order>> GetOrders(OrderStatus status, DateTime? from, DateTime? to, int itemsOnPage, int currentPage, OrderClause orderedBy)
        {
            var query = _repository
                .AllReadonly<PlacedOrder>();

            query = GetFilter(query, status, from, to);

            var orders = query
                .Include(po => po.PlacedOrderBooks)
                .ThenInclude(pb => pb.Book)
                .Include(o => o.User)
                .Select(o => new Order
                {
                    Id = o.Id,
                    City = o.City,
                    Address = o.Address,
                    OrderedOn = o.DatePlaced,
                    DeliveredOn = o.DateFulfilled,
                    Items = o.PlacedOrderBooks
                    .Select(pb => new OrderItem()
                    {
                        Id = pb.BookId,
                        Title = pb.Book.Title,
                        Quantity = pb.Quantity,
                        Price = pb.SingleItemPrice,
                        BookCover = pb.Book.BookCover
                    })
                    .ToList(),
                    TotalPrice = o.PlacedOrderBooks
                        .Sum(pb => pb.Quantity * pb.SingleItemPrice),
                    OrderStatus = !o.IsShipped && !o.DateFulfilled.HasValue
                        ? OrderStatus.Pending
                        : o.IsShipped && !o.DateFulfilled.HasValue
                        ? OrderStatus.Shipped
                        : OrderStatus.Delivered,
                    ClientName = $"{o.User.FirstName} {o.User.LastName}",
                    PhoneNumber = o.User.PhoneNumber,
                });

            orders = orderedBy switch
            {
                OrderClause.TotalPriceAsc => orders.OrderBy(o => o.TotalPrice),
                OrderClause.TotalPriceDesc => orders.OrderByDescending(o => o.TotalPrice),
                OrderClause.OrderDateAsc => orders.OrderBy(o => o.OrderedOn),
                _ => orders.OrderByDescending(o => o.OrderedOn)
            };
            var result = await orders.ToListAsync();

            var maxPages = await this.GetLastPage(status, from, to, itemsOnPage);

            if (currentPage < 1)
            {
                currentPage = 1;
            }

            if (maxPages > 0 && currentPage > maxPages)
            {
                currentPage = maxPages;
            }

            if (itemsOnPage < 1 || itemsOnPage > 10)
            {
                itemsOnPage = 10;
            }

            var skipCount = itemsOnPage * (currentPage - 1);

            result = result
                .Skip(skipCount)
                .Take(itemsOnPage)
                .ToList();

            return result;
        }

        /// <summary>
        /// Gets the last possible page of the search result.
        /// </summary>
        /// <param name="status">
        ///     <para>Selects an order by its status. There are 3 possible status types:</para>
        ///     <para>  -OrderStatus.Pending: When IsShipped is false and there's no Delivery date.</para>
        ///     <para>  -OrderStatus.Shipped: When IsShipped is true and there's no delivery date.</para>
        ///     <para>  -OrderStatus.Delivered: When IsShipped is true and there is a delivery date value.</para>
        /// </param>
        /// <param name="from">Start day of search.</param>
        /// <param name="to">End day of search.</param>
        /// <param name="itemsOnPage">Determines how many items to show in page.</param>
        /// <returns>Task&lt;int&gt;</returns>
        public async Task<int> GetLastPage(OrderStatus status, DateTime? from, DateTime? to, int itemsOnPage)
        {
            var query = _repository
                .AllReadonly<PlacedOrder>();

            query = GetFilter(query, status, from, to);

            return await query.CountAsync() / itemsOnPage;
        }

        /// <summary>
        /// An additional filtering query to reduce redundant typing for similar checks.
        /// </summary>
        /// <param name="query">Current query.</param>
        /// <param name="status">
        ///     <para>Selects an order by its status. There are 3 possible status types:</para>
        ///     <para>  -OrderStatus.Pending: When IsShipped is false and there's no Delivery date.</para>
        ///     <para>  -OrderStatus.Shipped: When IsShipped is true and there's no delivery date.</para>
        ///     <para>  -OrderStatus.Delivered: When IsShipped is true and there is a delivery date value.</para>
        /// </param>
        /// <param name="from">Start day of search.</param>
        /// <param name="to">End day of search.</param>
        /// <returns>IQueryable&lt;PlacedOrder&gt;</returns>
        private static IQueryable<PlacedOrder> GetFilter(IQueryable<PlacedOrder> query, OrderStatus status, DateTime? from, DateTime? to)
        {
            if (from.HasValue && to.HasValue && from < to)
            {
                query = query.Where(q => q.DatePlaced >= from && q.DatePlaced <= to);
            }

            query = status switch
            {
                OrderStatus.Delivered => query.Where(q => q.IsShipped && q.DateFulfilled != null),
                OrderStatus.Shipped => query.Where(q => q.IsShipped && q.DateFulfilled == null),
                _ => query.Where(q => q.IsShipped == false && q.DateFulfilled == null)
            };

            return query;
        }

        /// <summary>
        /// Flips the IsShipped flag of PlacedOrder to true.
        /// </summary>
        /// <param name="id">Key identifier.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<bool> MarkAsShipped(Guid id)
        {
            var order = await _repository
                .All<PlacedOrder>()
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                throw new InvalidOperationException(InvalidOrderId);
            }

            order.IsShipped = true;

            var result = await _repository.SaveChangesAsync() > 0;

            return result;
        }

        /// <summary>
        /// Adds DateTime.Now to DateFulfilled.
        /// </summary>
        /// <param name="id">Key identifier.</param>
        /// <returns>Task&lt;bool&gt;</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<bool> MarkAsDelivered(Guid id)
        {
            var order = await _repository
                .All<PlacedOrder>()
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                throw new InvalidOperationException(InvalidOrderId);
            }

            order.DateFulfilled = DateTime.Now;

            var result = await _repository.SaveChangesAsync() > 0;

            return result;
        }
    }
}
