namespace WebShop.Testing
{
    using Microsoft.EntityFrameworkCore;
    using Core.Contracts;
    using Core.Data;
    using Core.Repository;
    using Services.Contracts;
    using Services.ServiceControllers;
    using Datasets;
    public class BookShopServiceTests : BaseTestSetup
    {
        private BookShopService service;
        private IBookShopRepository bookShopRepository;

        [SetUp]
        public void Setup()
        {
            base.Setup<ApplicationDbContext>();
            bookShopRepository = new BookShopRepository((ApplicationDbContext)context);
            service = new BookShopService(bookShopRepository, mapper);
        }

        [Test]
        public async Task GetTopFiveOffers_ReturnsCorrectData()
        {
            await BookShopServiceDatasetSeeder.SeedFor_TopFive_Test(context);

            var result = await service.GetTopFiveOffers();

            var correctIds = new int[] { 1, 2, 3, 4, 6 };
            const int expectedCount = 5;

            Assert.That(expectedCount == result.Count, $"Expected item count was {expectedCount} but got {result.Count}");

            bool hasCorrectIds = true;
            foreach (var id in correctIds)
            {
                hasCorrectIds = result.FirstOrDefault(b => b.Id == id) != null;

                if (hasCorrectIds == false)
                {
                    break;
                }
            }

            Assert.IsTrue(hasCorrectIds, "Prices weren't ordered from low to high");

            var promoBook = result.First(b => b.Id == 6);
            var expectedPrice = 15.00m;

            Assert.That(expectedPrice == promoBook.CurrentPrice, "Promotion wasn't applied correctly");
        }

        [Test]
        public async Task GetCategoryIcons_ReturnsCorrectData()
        {
            await BookShopServiceDatasetSeeder.SeedFor_GenreIcons_Test(context);

            var result = await service.GetCategoryIcons();
            var expectedCount = 2;
            Assert.That(result.Count == expectedCount, $"Expected {expectedCount} but got {result.Count}");
        }

        [Test]
        public async Task GetCatalogue_ReturnsCorrectData()
        {
            await BookShopServiceDatasetSeeder.SeedFor_GetCatalogue_Test(context);

            string searchTerm = "Book";
            int itemsOnPage = 10;
            int genreId = 1;
            ItemSortClause sortBy = ItemSortClause.PriceAsc;
            int currentPage = 1;

            var result = await service.GetCatalogue(searchTerm, itemsOnPage, genreId, sortBy, currentPage);


            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.IsTrue(result[i].CurrentPrice <= result[i + 1].CurrentPrice);
            }

            searchTerm = "0000";
            result = await service.GetCatalogue(searchTerm, itemsOnPage, genreId, sortBy, currentPage);

            int expectedCount = 0;
            Assert.That(result.Count == expectedCount, "Search term filtering doesn't work");
        }

        [TestCase("", 10, 1, 2)]
        [TestCase("0000", 10, 1, 0)]
        [TestCase("", 20, 1, 1)]
        public async Task MaxPages_ReturnsCorrectCount(string searchTerm, int itemsOnPage, int genreId, int expectedCount)
        {
            await BookShopServiceDatasetSeeder.SeedFor_MaxPages_Test(context);

            var result = await service.MaxPages(searchTerm, itemsOnPage, genreId);
            Assert.That(expectedCount == result, $"Expected ${expectedCount} but got {result}");
        }

        [TestCase(5, 30.00)]
        [TestCase(20, 15.00)]
        [TestCase(30, 0.00)]
        public async Task BookInfo_ReturnsCorrectBook(int id, decimal expectedPrice)
        {
            await BookShopServiceDatasetSeeder.SeedFor_GetBookInfo_Test(context);

            var result = await service.GetBookInfo(id);
            ApplicationDbContext dbContext = (ApplicationDbContext)base.context;
            var expectedBook = await dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (expectedBook == null)
            {
                Assert.IsNull(result, "Value expected to be null.");
            }
            else
            {
                Assert.NotNull(result);
                Assert.That(result.Id == expectedBook.Id, $"Expected item with id {id} but got {result.Id}");
                Assert.That(result.CurrentPrice == expectedPrice, $"Invalid price expected {expectedPrice} but got {result.CurrentPrice}");
            }
        }

        [TestCase(5, true)]
        [TestCase(20, false)]
        public async Task AnyBook_ReturnsCorrectValue(int id, bool expectedValue)
        {
            await BookShopServiceDatasetSeeder.SeedFor_AnyBook_Test(context);

            var result = await service.AnyBook(id);

            Assert.That(result == expectedValue);
        }
    }
}
