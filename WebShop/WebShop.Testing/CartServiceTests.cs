namespace WebShop.Testing
{
    using Microsoft.EntityFrameworkCore;

    using Datasets;
    using Core.Data;
    using Core.Contracts;
    using Core.Repository;
    using Services.Models.BookShop;
    using Services.ServiceControllers;
    using static Services.ErrorMessages.CartErrors;

    public class CartServiceTests : BaseTestSetup
    {
        private IBookShopRepository bookShopRepository;
        private CartService _service;

        [SetUp]
        public void SetUp()
        {
            base.Setup<ApplicationDbContext>();
            bookShopRepository = new BookShopRepository((ApplicationDbContext)context);
            _service = new CartService(bookShopRepository);
        }

        [TestCase("1, 1|2, 1", 2, 75.00)]
        public async Task GetShopItems_ReturnsCorrectData(string data, int expectedItems, decimal expectedTotalPrice)
        {
            var tokens = data.Split('|');
            var items = new Dictionary<int, int>();

            foreach (var token in tokens)
            {
                var tokenData = token.Split(", ");
                int key = int.Parse(tokenData[0]);
                int value = int.Parse(tokenData[1]);
                items[key] = value;
            }

            await CartServiceDatasetSeeder.SeedFor_GetShopItems_Test(context);
            var result = await _service.GetShopItems(items);

            Assert.That(result.Count == expectedItems, GetErrorMsg(expectedItems, result.Count));

            var totalPrice = result.Sum(r => r.TotalPrice);
            Assert.That(totalPrice == expectedTotalPrice, GetErrorMsg(expectedTotalPrice, totalPrice));
        }

        [TestCase("1, 2|2, 1", 100.00)]
        public async Task GetTotalPrice_ReturnsCorrectValue(string data, decimal expectedTotalPrice)
        {
            var tokens = data.Split('|');
            var items = new Dictionary<int, int>();

            foreach (var token in tokens)
            {
                var tokenData = token.Split(", ");
                int key = int.Parse(tokenData[0]);
                int value = int.Parse(tokenData[1]);
                items[key] = value;
            }

            await CartServiceDatasetSeeder.SeedFor_GetShopItems_Test(context);
            var result = await _service.GetTotalPrice(items);

            Assert.That(result == expectedTotalPrice, GetErrorMsg(expectedTotalPrice, result));
        }

        [TestCase("1, 1|2, 1", true)]
        [TestCase("1, 1000|2, 1000", false)]
        [TestCase("1, 1|5, 1", false)]
        public async Task IsCartValid_ReturnsCorrectValue(string data, bool expected)
        {
            var tokens = data.Split('|');
            var items = new Dictionary<int, int>();

            foreach (var token in tokens)
            {
                var tokenData = token.Split(", ");
                int key = int.Parse(tokenData[0]);
                int value = int.Parse(tokenData[1]);
                items[key] = value;
            }

            await CartServiceDatasetSeeder.SeedFor_GetShopItems_Test(context);
            var result = await _service.IsCartValid(items);

            Assert.That(result == expected, GetErrorMsg(expected, result));
        }

        [Test]
        public async Task AddNewOrder_ThrowsError_WhenInvalidCart()
        {
            await CartServiceDatasetSeeder.SeedFor_AddNewOrder_Test(context);

            var model = new OrderModel()
            {
                Address = "Address",
                City = "City",
                Country = "Country"
            };

            var items = new Dictionary<int, int>()
            {
                { 0, 1 }
            };

            var userId = Guid.Parse("07fbc9e3-0d5f-4c5d-a1f7-ef1fd67f33c8");

            Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddNewOrder(items, model, userId));

            try
            {
                await _service.AddNewOrder(items, model, userId);
            }
            catch (Exception e)
            {
                var expectedMsg = InvalidCartInfo;
                Assert.That(e.Message == expectedMsg, GetErrorMsg(expectedMsg, e.Message));
            }
        }

        [Test]
        public async Task AddNewOrder_AddsCorrectInfo()
        {
            await CartServiceDatasetSeeder.SeedFor_AddNewOrder_Test(context);

            var model = new OrderModel()
            {
                Address = "Address",
                City = "City",
                Country = "Country"
            };

            var items = new Dictionary<int, int>()
            {
                { 1, 2 },
                { 2, 1 }
            };

            var userId = Guid.Parse("07fbc9e3-0d5f-4c5d-a1f7-ef1fd67f33c8");

            await _service.AddNewOrder(items, model, userId);
            var dbContext = (ApplicationDbContext)context;
            var order = await dbContext.PlacedOrders
                .Include(o => o.PlacedOrderBooks)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                Assert.Fail("Order wasn't added successfully to the context.");
            }

            var doesOrderContainItems = order.PlacedOrderBooks.Count(ob => ob.BookId == 1 || ob.BookId == 2) == 2;

            Assert.IsTrue(doesOrderContainItems, "No items were added to the order");

            var doesOrderMatch =
                model.Address == order.Address &&
                model.City == order.City &&
                model.Country == order.Country &&
                order.IsShipped == false &&
                order.UserId == userId &&
                order.PlacedOrderBooks.Sum(pb => pb.SingleItemPrice * pb.Quantity) == 100.00m;

            Assert.IsTrue(doesOrderMatch, "Order info didn't match");
        }

        [Test]
        public async Task GetInvoice_ReturnsNull_WhenNotFound()
        {
            await CartServiceDatasetSeeder.SeedFor_GetInvoice_Test(context);
            var result = await _service.GetCurrentInvoice(Guid.Parse("b596e9c4-9dcb-4826-8712-31d6d9b9e4c2"));
            Assert.IsNull(result, NullIsExpected);
        }

        [Test]
        public async Task GetInvoice_ReturnsCorrectInfo()
        {
            await CartServiceDatasetSeeder.SeedFor_GetInvoice_Test(context);
            var result = await _service.GetCurrentInvoice(Guid.Parse("07fbc9e3-0d5f-4c5d-a1f7-ef1fd67f33c8"));
            var dbContext = (ApplicationDbContext)context;

            var actualOrder = dbContext.PlacedOrders
                .Include(o => o.User)
                .First();

            var isIdentical =
                result.City == actualOrder.City &&
                result.Country == actualOrder.Country &&
                result.Address == actualOrder.Address &&
                result.TotalPrice == actualOrder.PlacedOrderBooks.Sum(pb => pb.SingleItemPrice * pb.Quantity) &&
                result.PhoneNumber == actualOrder.User.PhoneNumber &&
                result.CustomerName == $"{actualOrder.User.FirstName} {actualOrder.User.LastName}";

            Assert.IsTrue(isIdentical, "The order info isn't matching with the expected.");
        }
    }
}
