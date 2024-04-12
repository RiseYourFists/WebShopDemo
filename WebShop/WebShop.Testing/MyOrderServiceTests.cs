namespace WebShop.Testing
{
    using Moq;
    using System.Security.Claims;

    using Datasets;
    using DummyClasses;

    using Core.Contracts;
    using Core.Data;
    using Core.Repository;
    using Core.Models.Identity;

    using Services.ServiceControllers;
    using Services.Models.MyOrders.Enumerations;

    public class MyOrderServiceTests : BaseTestSetup
    {
        private IOrdersRepository repository;
        private MyOrderService _service;

        [SetUp]
        public void Setup()
        {
            base.Setup<ApplicationDbContext>();
            repository = new OrdersRepository((ApplicationDbContext)context);


            var userHelperMock = new Mock<UserHelper<ApplicationUser, Guid>>(UserHelperMockSetup.UserManagerMock.Object, UserHelperMockSetup.SignInManagerMock.Object);

            userHelperMock.ConfigureMock();

            var userHelper = userHelperMock.Object;
            _service = new MyOrderService(repository, userHelper);
        }

        [Test]
        public async Task GetActiveOrderCount_ReturnsCorrectCount()
        {
            var user = new ClaimsPrincipal();
            await MyOrderServiceDatasets.SeedFor_GetOrderCount_Test(context);
            var result = await _service.GetActiveOrderCount(user);
            Assert.That(result == 2);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task AnyUserOrdersPresent_ReturnsCorrectValue(bool negativeTest)
        {
            if (negativeTest)
            {
                await MyOrderServiceDatasets.SeedFor_AnyUserOrdersPresent_TestPositive(context);
                var result = await _service.AnyUserOrdersPresent(new ClaimsPrincipal());
                Assert.IsTrue(result);
            }
            else
            {
                await MyOrderServiceDatasets.SeedFor_AnyUserOrdersPresent_TestNegative(context);
                var result = await _service.AnyUserOrdersPresent(new ClaimsPrincipal());
                Assert.IsFalse(result);
            }
        }

        [TestCase("01-01-0001", "12-31-9999", OrderStatus.All, OrderClause.TotalPriceDesc, 3, new[] { OrderStatus.Shipped, OrderStatus.Pending, OrderStatus.Delivered })]
        [TestCase("01-01-0001", "12-31-9999", OrderStatus.All, OrderClause.TotalPriceAsc, 3, new[] { OrderStatus.Shipped, OrderStatus.Pending, OrderStatus.Delivered })]
        [TestCase("01-01-0001", "12-31-9999", OrderStatus.All, OrderClause.OrderDateDesc, 3, new[] { OrderStatus.Shipped, OrderStatus.Pending, OrderStatus.Delivered })]
        [TestCase("01-01-0001", "12-31-9999", OrderStatus.All, OrderClause.OrderDateAsc, 3, new[] { OrderStatus.Shipped, OrderStatus.Pending, OrderStatus.Delivered })]
        [TestCase("01-01-0001", "12-31-9999", OrderStatus.Pending, OrderClause.TotalPriceDesc, 1, new[] { OrderStatus.Pending })]
        [TestCase("01-01-0001", "12-31-9999", OrderStatus.Shipped, OrderClause.TotalPriceDesc, 1, new[] { OrderStatus.Shipped })]
        [TestCase("01-01-0001", "12-31-9999", OrderStatus.Delivered, OrderClause.TotalPriceDesc, 1, new[] { OrderStatus.Delivered })]
        [TestCase("11-30-9999", "12-31-9999", OrderStatus.Delivered, OrderClause.TotalPriceDesc, 0, new[] { OrderStatus.Delivered })]
        public async Task GetUserOrders_ReturnsCorrectInfo(
            string from,
            string to,
            OrderStatus status,
            OrderClause order,
            int expectedCount,
            OrderStatus[] validStatus)
        {
            var fromDate = DateTime.Parse(from);
            var toDate = DateTime.Parse(to);

            await MyOrderServiceDatasets.SeedFor_GetUserOrders_Test(context);
            var result = await _service.GetUserOrders(new ClaimsPrincipal(), status, order, fromDate, toDate);

            Assert.That(result.Count == expectedCount);
            if (result.Count > 0)
            {
                foreach (var orderItem in result)
                {
                    Assert.IsTrue(validStatus.Contains(orderItem.OrderStatus), $"Expected {string.Join("/ ", validStatus)} but got {orderItem.OrderStatus}");
                }
            }

            if (order == OrderClause.TotalPriceDesc)
            {
                var prev = decimal.MaxValue;
                foreach (var item in result)
                {
                    if (item.TotalPrice <= prev)
                    {
                        prev = item.TotalPrice;
                    }
                    else
                    {
                        Assert.Fail($"Expected order: {order} But result was: {string.Join(" > ", result.Select(r => r.TotalPrice))}");
                    }
                }
                Assert.Pass();
            }
            else if (order == OrderClause.TotalPriceAsc)
            {
                var prev = decimal.MinValue;
                foreach (var item in result)
                {
                    if (item.TotalPrice >= prev)
                    {
                        prev = item.TotalPrice;
                    }
                    else
                    {
                        Assert.Fail($"Expected order: {order} But result was: {string.Join(" > ", result.Select(r => r.TotalPrice))}");
                    }
                }
                Assert.Pass();
            }
            else if (order == OrderClause.OrderDateDesc)
            {
                var prev = DateTime.MaxValue;
                foreach (var item in result)
                {
                    if (item.OrderedOn <= prev)
                    {
                        prev = item.OrderedOn;
                    }
                    else
                    {
                        Assert.Fail($"Expected order: {order} But result was: {string.Join(" > ", result.Select(r => r.TotalPrice))}");
                    }
                }
                Assert.Pass();
            }
            else if (order == OrderClause.OrderDateAsc)
            {
                var prev = DateTime.MinValue;
                foreach (var item in result)
                {
                    if (item.OrderedOn >= prev)
                    {
                        prev = item.OrderedOn;
                    }
                    else
                    {
                        Assert.Fail($"Expected order: {order} But result was: {string.Join(" > ", result.Select(r => r.TotalPrice))}");
                    }
                }
                Assert.Pass();
            }
        }
    }
}
