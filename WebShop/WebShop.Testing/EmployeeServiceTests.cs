namespace WebShop.Testing
{
    using Datasets;
    using Core.Data;
    using Core.Contracts;
    using Core.Repository;
    using Services.ServiceControllers;
    using Services.Models.MyOrders.Enumerations;

    public class EmployeeServiceTests : BaseTestSetup
    {
        private IEmployeeRepository _employeeRepository;
        private EmployeeService _employeeService;

        [SetUp]
        public void SetUp()
        {
            base.Setup<ApplicationDbContext>();
            _employeeRepository = new EmployeeRepository((ApplicationDbContext)context);
            _employeeService = new EmployeeService(_employeeRepository);
        }

        [TestCase("01-01-0001", "12-31-9999", OrderStatus.Pending, 1, 1, OrderClause.TotalPriceDesc, 1)]
        [TestCase("01-01-0001", "12-31-9999", OrderStatus.Pending, 10, 1, OrderClause.TotalPriceDesc, 2)]
        [TestCase("01-01-0001", "12-31-9999", OrderStatus.Pending, 10, 1, OrderClause.TotalPriceAsc, 2)]
        [TestCase("01-01-0001", "12-31-9999", OrderStatus.Pending, 10, 1, OrderClause.OrderDateAsc, 2)]
        [TestCase("01-01-0001", "12-31-9999", OrderStatus.Pending, 10, 1, OrderClause.OrderDateDesc, 2)]
        [TestCase("01-01-0001", "12-31-9999", OrderStatus.Shipped, 10, 1, OrderClause.TotalPriceDesc, 1)]
        [TestCase("01-01-0001", "12-31-9999", OrderStatus.Delivered, 10, 1, OrderClause.TotalPriceDesc, 1)]
        public async Task GetOrders_ReturnsCorrectInfo(string from, string to, OrderStatus status, int itemsOnPage, int currentPage, OrderClause orderedBy, int expectedCount)
        {
            await EmployeeServiceDatasetSeeder.SeedFor_GetOrders_Tests(context);

            var fromDate = DateTime.Parse(from);
            var toDate = DateTime.Parse(to);

            var result = await _employeeService.GetOrders(status, fromDate, toDate, itemsOnPage, currentPage, orderedBy);

            Assert.That(result.Count == expectedCount, GetErrorMsg(expectedCount, result.Count));

            if (result.Count > 1)
            {
                if (orderedBy == OrderClause.TotalPriceAsc)
                {
                    var previous = decimal.MaxValue;
                    foreach (var order in result)
                    {
                        if (order.TotalPrice <= previous)
                        {
                            previous = order.TotalPrice;
                        }
                        else
                        {
                            Assert.Fail();
                        }
                    }
                }
                else if (orderedBy == OrderClause.TotalPriceDesc)
                {
                    var previous = decimal.MinValue;
                    foreach (var order in result)
                    {
                        if (order.TotalPrice >= previous)
                        {
                            previous = order.TotalPrice;
                        }
                        else
                        {
                            Assert.Fail();
                        }
                    }
                }
                else if (orderedBy == OrderClause.OrderDateAsc)
                {
                    var previous = DateTime.MaxValue;
                    foreach (var order in result)
                    {
                        if (order.OrderedOn <= previous)
                        {
                            previous = order.OrderedOn;
                        }
                        else
                        {
                            Assert.Fail();
                        }
                    }
                }
                else if (orderedBy == OrderClause.OrderDateDesc)
                {
                    var previous = DateTime.MinValue;
                    foreach (var order in result)
                    {
                        if (order.OrderedOn >= previous)
                        {
                            previous = order.OrderedOn;
                        }
                        else
                        {
                            Assert.Fail();
                        }
                    }
                }
            }
        }

        [Test]
        public async Task GetOrders_PaginationWorksProperly()
        {
            await EmployeeServiceDatasetSeeder.SeedFor_GetOrders_Tests(context);

            var result = await _employeeService.GetOrders(OrderStatus.Pending, DateTime.MinValue, DateTime.MaxValue, 1,
                1, OrderClause.OrderDateAsc);

            Assert.That(result[0].Id == Guid.Parse("3f0a7a8e-9aa5-4f18-8b11-6ec60ab1f94f"), GetErrorMsg(result[0].Id, Guid.Parse("3f0a7a8e-9aa5-4f18-8b11-6ec60ab1f94f")));
            result = await _employeeService.GetOrders(OrderStatus.Pending, DateTime.MinValue, DateTime.MaxValue, 1, 2,
                OrderClause.OrderDateAsc);

            Assert.That(result[0].Id == Guid.Parse("3f0a7a8e-9aa5-4f18-8b41-6ec50ab1f94b"), GetErrorMsg(result[0].Id, Guid.Parse("3f0a7a8e-9aa5-4f18-8b41-6ec50ab1f94b")));

        }

        [Test]
        public async Task GetOrders_TimeFilteringWorks()
        {
            await EmployeeServiceDatasetSeeder.SeedFor_GetOrders_Tests(context);

            var result = await _employeeService.GetOrders(OrderStatus.Delivered, DateTime.Now, DateTime.MaxValue, 10, 1,
                OrderClause.OrderDateAsc);

            var expectedCount = 0;
            Assert.That(result.Count == expectedCount, GetErrorMsg(expectedCount, result.Count));

            expectedCount = 1;
            result = await _employeeService.GetOrders(OrderStatus.Delivered, DateTime.MinValue, DateTime.MaxValue, 10,
                1, OrderClause.OrderDateAsc);

            Assert.That(result.Count == expectedCount, GetErrorMsg(expectedCount, result.Count));
        }

        [TestCase("01-01-0001", "12-31-9999", OrderStatus.Pending, 1, 2)]
        [TestCase("01-01-0001", "12-31-9999", OrderStatus.Pending, 2, 1)]
        [TestCase("01-01-9998", "12-31-9999", OrderStatus.Delivered, 10, 0)]
        [TestCase("01-01-9998", "12-31-9999", OrderStatus.Delivered, 1, 0)]
        public async Task GetLastPage_ReturnsCorrectValue(string from, string to, OrderStatus status, int itemsOnPage, int expectedCount)
        {
            var fromDate = DateTime.Parse(from);
            var toDate = DateTime.Parse(to);

            await EmployeeServiceDatasetSeeder.SeedFor_GetLastPage_Tests(context);
            var result = await _employeeService.GetLastPage(status, fromDate, toDate, itemsOnPage);

            Assert.That(result == expectedCount, GetErrorMsg(expectedCount, result));
        }

        [Test]
        public async Task MarkAsShipped_IsApplied()
        {
            await EmployeeServiceDatasetSeeder.SeedFor_MarkAsShipped_Tests(context);
            var id = Guid.Parse("3f0a7a8e-9aa5-4f18-8b41-6ec50ab1f94b");

            await _employeeService.MarkAsShipped(id);

            var dbContext = (ApplicationDbContext)context;
            var result = dbContext.PlacedOrders.FirstOrDefault(o => o.Id == id);
            if (result == null)
            {
                Assert.Fail();
            }

            Assert.IsTrue(result!.IsShipped);
        }

        [Test]
        public async Task MarkAsShipped_ThrowsInvalidException()
        {
            await EmployeeServiceDatasetSeeder.SeedFor_MarkAsShipped_Tests(context);
            var id = Guid.Parse("3f0a7a8e-9aa5-4f18-8b41-6ec50ab1f94a");

            Assert.ThrowsAsync<InvalidOperationException>(() => _employeeService.MarkAsShipped(id));

            var expectedMsg = "Invalid order id.";

            try
            {
                await _employeeService.MarkAsShipped(id);
            }
            catch (Exception e)
            {
                Assert.That(e.Message == expectedMsg, GetErrorMsg(expectedMsg, e.Message));
            }
        }

        [Test]
        public async Task MarkAsDelivered_IsApplied()
        {
            await EmployeeServiceDatasetSeeder.SeedFor_MarkAsShipped_Tests(context);
            var id = Guid.Parse("3f0a7a8e-9aa5-4f18-8b41-6ec50ab1f94b");

            await _employeeService.MarkAsDelivered(id);

            var dbContext = (ApplicationDbContext)context;
            var result = dbContext.PlacedOrders.FirstOrDefault(o => o.Id == id);
            if (result == null)
            {
                Assert.Fail();
            }

            Assert.IsTrue(result!.DateFulfilled != null);
        }

        [Test]
        public async Task MarkAsDelivered_ThrowsInvalidException()
        {
            await EmployeeServiceDatasetSeeder.SeedFor_MarkAsShipped_Tests(context);
            var id = Guid.Parse("3f0a7a8e-9aa5-4f18-8b41-6ec50ab1f94a");

            Assert.ThrowsAsync<InvalidOperationException>(() => _employeeService.MarkAsDelivered(id));

            var expectedMsg = "Invalid order id.";

            try
            {
                await _employeeService.MarkAsDelivered(id);
            }
            catch (Exception e)
            {
                Assert.That(e.Message == expectedMsg, GetErrorMsg(expectedMsg, e.Message));
            }
        }
    }
}
