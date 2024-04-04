using System.Security.Claims;
using WebShop.Core.Contracts;
using WebShop.Core.Data;
using WebShop.Core.Repository;
using WebShop.Services.Models.MyOrders.Enumerations;
using WebShop.Services.ServiceControllers;
using WebShop.Testing.DummyClasses;

namespace WebShop.Testing
{
    using Datasets;
    using Microsoft.Extensions.DependencyInjection;

    public class MyOrderServiceTests : BaseTestSetup
    {
        private IOrdersRepository repository;
        private MyOrderService _service;

        [SetUp]
        public void Setup()
        {
            base.Setup<ApplicationDbContext>();
            repository = new OrdersRepository((ApplicationDbContext)context);
            var userHelper = new UserHelperMock();
            _service = new MyOrderService(repository, userHelper);
        }

        [Test]
        public async Task Test1()
        {
            var user = new ClaimsPrincipal();
            var result = await _service.GetUserOrders(user, OrderStatus.All, OrderClause.TotalPriceAsc, DateTime.MinValue, DateTime.MaxValue);

            Assert.That(result.Count == 0);
        }

        
    }
}
