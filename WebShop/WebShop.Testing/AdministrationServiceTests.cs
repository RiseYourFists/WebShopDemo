namespace WebShop.Testing
{
    using Moq;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using Core.Data;
    using Datasets;
    using DummyClasses;
    using Core.Contracts;
    using Core.Repository;
    using Core.Models.Identity;
    using Services.ServiceControllers;
    using Services.Models.Administration;
    using Services.Models.Administration.Enumerations;
    using static Services.ErrorMessages.AdministrationErrors;

    public class AdministrationServiceTests : BaseTestSetup
    {
        private IAdminRepository adminRepository;
        private AdministrationService _adminService;

        [SetUp]
        public void Setup()
        {
            base.Setup<ApplicationDbContext>();
            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            userManagerMock = userManagerMock.ConfigureMock((ApplicationDbContext)context);

            adminRepository = new AdminRepository((ApplicationDbContext)context);
            _adminService = new AdministrationService(mapper, adminRepository, userManagerMock.Object);
        }

        [Test]
        public async Task GetGenres_ReturnsCorrectInfo()
        {
            var additionalGenreCountForAll = 1;
            var expectedCount = await AdministrationServiceDatasetSeeder.SeedFor_GetGenres_Tests(context) + additionalGenreCountForAll;

            var result = await _adminService.GetGenres();
            Assert.That(expectedCount == result.Count, GetErrorMsg(expectedCount, result.Count));
        }

        [Test]
        public async Task GetAuthors_ReturnsCorrectInfo()
        {
            var additionalAuthorsForAll = 1;
            var expectedCount = await AdministrationServiceDatasetSeeder.SeedFor_GetAuthors_Tests(context) + additionalAuthorsForAll;
            var result = await _adminService.GetAuthors();

            Assert.That(expectedCount == result.Count, GetErrorMsg(expectedCount, result.Count));
        }

        [Test]
        public async Task GetBooks_CalculatesPromotionsCorrectly()
        {
            await AdministrationServiceDatasetSeeder.SeedFor_GetBooks_Tests(context);
            var result = await _adminService.GetBooks(string.Empty, 0, 0);
            var promoBook = result[0];
            var expectedPrice = 15m;
            Assert.That(promoBook.CurrentPrice == expectedPrice, GetErrorMsg(expectedPrice, promoBook.CurrentPrice));
        }

        [TestCase(null, 0, 0, 10)]
        [TestCase(null, 1, 0, 1)]
        [TestCase(null, 0, 1, 1)]
        [TestCase("Title10", 0, 0, 1)]
        public async Task GetBooks_FiltersAreWorkingProperly(string searchTerm, int authorId, int genreId, int expectedCount)
        {
            await AdministrationServiceDatasetSeeder.SeedFor_GetBooks_Tests(context);
            var result = await _adminService.GetBooks(searchTerm, authorId, genreId);

            Assert.That(expectedCount == result.Count, GetErrorMsg(expectedCount, result.Count));
        }

        [Test]
        public async Task GetGenresSelectionItem_ReturnsCorrectInfo()
        {
            var expectedCount = await AdministrationServiceDatasetSeeder.SeedFor_GetGenres_Tests(context);
            var result = await _adminService.GetGenresSelectionItem();
            Assert.That(expectedCount == result.Count, GetErrorMsg(expectedCount, result.Count));
        }

        [TestCase(0, true)]
        [TestCase(1, false, "Title1")]
        public async Task GetBookInfo_ReturnsCorrectInfo(int bookId, bool isNullExpected, string bookTitleExpected = null)
        {
            await AdministrationServiceDatasetSeeder.SeedFor_GetBooks_Tests(context);
            var result = await _adminService.GetBookInfo(bookId);
            if (isNullExpected)
            {
                Assert.IsNull(result, NullIsExpected);
            }
            else
            {
                if (result == null)
                {
                    Assert.Fail(ObjectNotNull);
                }

                Assert.That(result.Title == bookTitleExpected, GetErrorMsg(bookTitleExpected, result.Title));
            }

        }

        [TestCase(0, false)]
        [TestCase(1, true)]
        public async Task AnyGenre_ReturnsCorrectValue(int id, bool expectedValue)
        {
            await AdministrationServiceDatasetSeeder.SeedFor_AnyGenre_Tests(context);
            var result = await _adminService.AnyGenre(id);
            Assert.That(result == expectedValue, GetErrorMsg(expectedValue, result));
        }

        [TestCase(0, false)]
        [TestCase(1, true)]
        public async Task AnyAuthor_ReturnsCorrectValue(int id, bool expectedValue)
        {
            await AdministrationServiceDatasetSeeder.SeedFor_AnyAuthor_Tests(context);
            var result = await _adminService.AnyAuthor(id);
            Assert.That(result == expectedValue, GetErrorMsg(expectedValue, result));
        }

        [Test]
        public async Task GetAuthorsSelectionItem_ReturnsCorrectInfo()
        {
            var expectedCount = await AdministrationServiceDatasetSeeder.SeedFor_GetAuthors_Tests(context);
            var result = await _adminService.GetAuthorsSelectionItem();
            Assert.That(expectedCount == result.Count, GetErrorMsg(expectedCount, result.Count));
        }


        [TestCase(0, true)]
        [TestCase(1, false, "Title1")]
        public async Task GetBook_ReturnsCorrectInfo(int id, bool isNullExpected, string expectedTitle = null)
        {
            await AdministrationServiceDatasetSeeder.SeedFor_GetBook_Tests(context);
            var result = await _adminService.GetBook(id);

            if (isNullExpected)
            {
                Assert.IsNull(result, NullIsExpected);
            }
            else
            {
                if (result == null)
                {
                    Assert.Fail(ObjectNotNull);
                }

                Assert.That(result.Title == expectedTitle, GetErrorMsg(expectedTitle, result.Title));
            }
        }

        [Test]
        public async Task EditBook_AppliesNewValues()
        {
            await AdministrationServiceDatasetSeeder.SeedFor_GetBook_Tests(context);

            var model = new BookInfoModel()
            {
                Id = 1,
                Action = EditAction.Edit,
                AuthorId = 1,
                GenreId = 1,
                Title = "Title1-Edited",
                Description = "Empty",
                BookCover = "Empty",
                BasePrice = 15m
            };

            var result = await _adminService.EditBookInfo(model);

            Assert.IsTrue(result, "The edit is supposed to be successful!");

            var dbContext = (ApplicationDbContext)context;
            var bookResult = await dbContext
                .Books
                .FirstAsync();

            Assert.That(bookResult.Title == model.Title && bookResult.BasePrice == model.BasePrice, GetErrorMsg($"{model.Title}, {model.BasePrice}", $"{bookResult.Title}, {bookResult.BasePrice}"));
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task EditBook_ThrowsException(int testCase)
        {
            await AdministrationServiceDatasetSeeder.SeedFor_EditBook_Tests(context);

            if (testCase == 1)
            {
                Assert.ThrowsAsync<InvalidOperationException>(async () => await _adminService.EditBookInfo(new()));

                try
                {
                    await _adminService.EditBookInfo(new());
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == InvalidBookId, GetErrorMsg(InvalidBookId, e.Message));
                }
            }
            else
            {
                var model = new BookInfoModel()
                {
                    Id = 1,
                    GenreId = 0,
                    AuthorId = 0
                };

                Assert.ThrowsAsync<InvalidOperationException>(async () => await _adminService.EditBookInfo(model));
            }
        }

        [TestCase(0, true)]
        [TestCase(1, false, "Genre1")]
        public async Task GetGenreInfo_ReturnsCorrectInfo(int id, bool isNullExpected, string nameExpected = null)
        {
            await AdministrationServiceDatasetSeeder.SeedFor_GetGenreInfo_Tests(context);
            var result = await _adminService.GetGenreInfo(id);

            if (isNullExpected)
            {
                Assert.IsNull(result, NullIsExpected);
            }
            else
            {
                if (result == null)
                {
                    Assert.Fail(ObjectNotNull);
                }

                Assert.That(result.Name == nameExpected, GetErrorMsg(nameExpected, result.Name));
            }
        }

        [Test]
        public async Task AddGenre_AddsNewGenre()
        {
            var genre = new GenreEditorModel()
            {
                IconLink = "Empty",
                Name = "Genre1"
            };

            Assert.IsTrue(await _adminService.AddNewGenre(genre));

            var dbContext = (ApplicationDbContext)context;
            var result = await dbContext.Genres.FirstAsync();

            Assert.That(result.Id == 1 && result.Name == genre.Name, GetErrorMsg($"1, {genre.Name}", $"{result.Id}, {result.Name}"));
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task EditGenre_WorksProperly(bool throwCase)
        {
            await AdministrationServiceDatasetSeeder.SeedFor_GetGenreInfo_Tests(context);
            if (throwCase)
            {
                var model = new GenreEditorModel
                {
                    Id = 0,
                    Name = "Genre1",
                    IconLink = "Empty"
                };

                Assert.ThrowsAsync<InvalidOperationException>(async () => await _adminService.EditGenre(model));

                try
                {
                    await _adminService.EditGenre(model);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == InvalidGenreId, GetErrorMsg(InvalidGenreId, e.Message));
                }
            }
            else
            {
                var model = new GenreEditorModel()
                {
                    Id = 1,
                    Name = "Genre1-Edited",
                    IconLink = "Empty"
                };

                Assert.IsTrue(await _adminService.EditGenre(model));

                var dbContext = (ApplicationDbContext)context;
                var result = await dbContext.Genres.FirstAsync();

                Assert.That(result.Name == model.Name, GetErrorMsg(model.Name, result.Name));
            }
        }

        [TestCase(0, true)]
        [TestCase(1, false, "Author1")]
        public async Task GetAuthorInfo_ReturnsCorrectInfo(int id, bool isNullExpected, string nameExpected = null)
        {
            await AdministrationServiceDatasetSeeder.SeedFor_GetAuthorInfo_Tests(context);
            var result = await _adminService.GetAuthorInfo(id);

            if (isNullExpected)
            {
                Assert.IsNull(result, NullIsExpected);
            }
            else
            {
                if (result == null)
                {
                    Assert.Fail(ObjectNotNull);
                }

                Assert.That(result.Name == nameExpected, GetErrorMsg(nameExpected, result.Name));
            }
        }

        [Test]
        public async Task AddAuthor_AddsNewAuthor()
        {
            var author = new AuthorEditorModel()
            {
                Name = "Genre1"
            };

            Assert.IsTrue(await _adminService.AddNewAuthor(author));

            var dbContext = (ApplicationDbContext)context;
            var result = await dbContext.Authors.FirstAsync();

            Assert.That(result.Id == 1 && result.Name == author.Name, GetErrorMsg($"1, {author.Name}", $"{result.Id}, {result.Name}"));
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task EditAuthor_WorksProperly(bool throwCase)
        {
            await AdministrationServiceDatasetSeeder.SeedFor_GetAuthorInfo_Tests(context);
            if (throwCase)
            {
                var model = new AuthorEditorModel()
                {
                    Id = 0,
                    Name = "Author1"
                };

                Assert.ThrowsAsync<InvalidOperationException>(async () => await _adminService.EditAuthor(model));

                try
                {
                    await _adminService.EditAuthor(model);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == InvalidAuthorId, GetErrorMsg(InvalidAuthorId, e.Message));
                }
            }
            else
            {
                var model = new AuthorEditorModel()
                {
                    Id = 1,
                    Name = "Author1-Edited"
                };

                Assert.IsTrue(await _adminService.EditAuthor(model));

                var dbContext = (ApplicationDbContext)context;
                var result = await dbContext.Authors.FirstAsync();

                Assert.That(result.Name == model.Name, GetErrorMsg(model.Name, result.Name));
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public async Task AddNewBook_WorksProperly(bool throwCase)
        {
            await AdministrationServiceDatasetSeeder.SeedFor_AddNewBook_Tests(context);

            var model = new BookInfoModel()
            {
                Title = "Title1",
                Description = "Empty",
                BasePrice = 30,
                BookCover = "Empty",
                Action = EditAction.Add,
                GenreId = 1,
                AuthorId = 1
            };

            if (throwCase)
            {
                model.GenreId = 0;
                Assert.ThrowsAsync<InvalidOperationException>(async () => await _adminService.AddNewBook(model));

                try
                {
                    await _adminService.AddNewBook(model);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == InvalidGenreAuthorId, GetErrorMsg(InvalidGenreAuthorId, e.Message));
                }

                model.GenreId = 1;
                model.AuthorId = 0;
                Assert.ThrowsAsync<InvalidOperationException>(async () => await _adminService.AddNewBook(model));

                try
                {
                    await _adminService.AddNewBook(model);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == InvalidGenreAuthorId, GetErrorMsg(InvalidGenreAuthorId, e.Message));
                }
            }
            else
            {
                Assert.IsTrue(await _adminService.AddNewBook(model));

                var dbContext = (ApplicationDbContext)context;
                var result = await dbContext.Books.FirstAsync();

                Assert.That(result.Title == model.Title, GetErrorMsg(model.Title, result.Title));
            }
        }

        [TestCase(null, 10)]
        [TestCase("Promotion10", 1)]
        public async Task GetPromotions_ReturnsCorrectInfo(string searchTerm, int expectedCount)
        {
            await AdministrationServiceDatasetSeeder.SeedFor_GetPromotions_Tests(context);

            var result = await _adminService.GetPromotions(searchTerm);

            Assert.That(result.Count == expectedCount, GetErrorMsg(expectedCount, result.Count));
        }

        [TestCase(0, true)]
        [TestCase(1, false, "Promotion1")]
        public async Task GetPromotion_ReturnsCorrectInfo(int id, bool isNullExpected, string expectedName = null)
        {
            await AdministrationServiceDatasetSeeder.SeedFor_GetPromotion_Tests(context);
            var result = await _adminService.GetPromotion(id);

            if (isNullExpected)
            {
                Assert.IsNull(result, NullIsExpected);
            }
            else
            {
                if (result == null)
                {
                    Assert.Fail(ObjectNotNull);
                }

                Assert.That(result.Name == expectedName, GetErrorMsg(expectedName, result.Name));
            }
        }

        [Test]
        public async Task GetPromotionAuthors_ReturnsCorrectInfo()
        {
            var expectedCount = await AdministrationServiceDatasetSeeder.SeedFor_GetPromotionAuthors_Tests(context);
            var result = await _adminService.GetPromotionAuthors();

            Assert.That(expectedCount == result.Count, GetErrorMsg(expectedCount, result.Count));

            var areAllValid = true;

            foreach (var auth in result)
            {
                var value = (string)auth.PropertyValue;

                if (value.StartsWith("author-") == false)
                {
                    areAllValid = false;
                    break;
                }
            }

            Assert.IsTrue(areAllValid, "Property values must start with 'author-'");
        }

        [Test]
        public async Task GetPromotionGenres_ReturnsCorrectInfo()
        {
            var expectedCount = await AdministrationServiceDatasetSeeder.SeedFor_GetPromotionGenres_Tests(context);
            var result = await _adminService.GetPromotionGenres();

            Assert.That(expectedCount == result.Count, GetErrorMsg(expectedCount, result.Count));

            var areAllValid = true;

            foreach (var auth in result)
            {
                var value = (string)auth.PropertyValue;

                if (value.StartsWith("genre-") == false)
                {
                    areAllValid = false;
                    break;
                }
            }

            Assert.IsTrue(areAllValid, "Property values must start with 'genre-'");
        }

        [Test]
        public async Task RemovePromotions_WorksProperly()
        {
            await AdministrationServiceDatasetSeeder.SeedFor_RemovePromotions_Tests(context);

            Assert.IsTrue(await _adminService.RemovePromotions(1));

            var dbContext = (ApplicationDbContext)context;

            var isAuthorPromotionClear = !await dbContext.AuthorPromotions.AnyAsync();
            var isGenrePromotionClear = !await dbContext.GenrePromotions.AnyAsync();

            Assert.IsTrue(isGenrePromotionClear && isAuthorPromotionClear);
        }

        [Test]
        public async Task EditPromotion_AppliesNewChanges()
        {
            var model = new PromotionEditorModel()
            {
                Id = 1,
                Action = EditAction.Edit,
                DiscountPercent = 40,
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
                Name = "Promotion1-Edited",
                PromotionType = "author-2"
            };
            await AdministrationServiceDatasetSeeder.SeedFor_AddAndEditPromotion_Tests(context);

            Assert.IsTrue(await _adminService.EditPromotion(model), "No changes applied");

            var dbContext = (ApplicationDbContext)context;
            var result = await dbContext.Promotions
                .AsNoTracking()
                .Include(p => p.GenrePromotions)
                .Include(p => p.AuthorPromotions)
                .FirstAsync(p => p.Id == model.Id);

            var isPromotionTypeChanged = result.AuthorPromotions.Any(ap => ap.AuthorId == 2) &&
                                         result.GenrePromotions.Any() == false;

            Assert.IsTrue(isPromotionTypeChanged);

            var arePropertiesChanged = model.Name == result.Name && model.DiscountPercent == result.DiscountPercent;

            Assert.IsTrue(arePropertiesChanged);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public async Task EditPromotion_Throws(int testCase)
        {
            var model = new PromotionEditorModel()
            {
                Id = 1,
                Action = EditAction.Edit,
                DiscountPercent = 40,
                StartDate = DateTime.Parse("01-01-2012"),
                EndDate = DateTime.Now,
                Name = "Promotion1-Edited",
                PromotionType = "author-1"
            };

            await AdministrationServiceDatasetSeeder.SeedFor_AddAndEditPromotion_Tests(context);

            if (testCase == 1)
            {
                model.PromotionType = "author1";

                Assert.ThrowsAsync<ArgumentException>(async () => await _adminService.EditPromotion(model));

                try
                {
                    await _adminService.EditPromotion(model);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == InvalidPromotionIdFormat, GetErrorMsg(InvalidPromotionIdFormat, e.Message));
                }
            }
            else if (testCase == 2)
            {
                model.Id = 20;

                Assert.ThrowsAsync<InvalidOperationException>(async () => await _adminService.EditPromotion(model));

                try
                {
                    await _adminService.EditPromotion(model);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == InvalidPromotionId, GetErrorMsg(InvalidPromotionId, e.Message));
                }
            }
            else if(testCase == 3)
            {
                model.PromotionType = "person-1";

                Assert.ThrowsAsync<ArgumentException>(async () => await _adminService.EditPromotion(model));

                try
                {
                    await _adminService.EditPromotion(model);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == InvalidPromotionIdFormat, GetErrorMsg(InvalidPromotionIdFormat, e.Message));
                }
            }
            else if (testCase == 4)
            {
                model.PromotionType = "author-10";

                Assert.ThrowsAsync<InvalidOperationException>(async () => await _adminService.EditPromotion(model));

                try
                {
                    await _adminService.EditPromotion(model);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == GenreAuthorNotFound, GetErrorMsg(GenreAuthorNotFound, e.Message));
                }
            }
            else
            {
                Assert.ThrowsAsync<ArgumentException>(async () => await _adminService.EditPromotion(model));

                try
                {
                    await _adminService.EditPromotion(model);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == ExistingPromotions, GetErrorMsg(ExistingPromotions, e.Message));
                }
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task AddPromotion_Throws(int testCase)
        {
            var model = new PromotionEditorModel()
            {
                Action = EditAction.Add,
                DiscountPercent = 40,
                StartDate = DateTime.Parse("01-01-2012"),
                EndDate = DateTime.Now,
                Name = "Promotion1-Edited",
                PromotionType = "author-1"
            };

            await AdministrationServiceDatasetSeeder.SeedFor_AddAndEditPromotion_Tests(context);

            if (testCase == 1)
            {
                model.PromotionType = "author1";

                Assert.ThrowsAsync<ArgumentException>(async () => await _adminService.AddPromotion(model));

                try
                {
                    await _adminService.AddPromotion(model);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == InvalidPromotionIdFormat, GetErrorMsg(InvalidPromotionIdFormat, e.Message));
                }
            }
            else if (testCase == 2)
            {
                model.PromotionType = "person-1";

                Assert.ThrowsAsync<ArgumentException>(async () => await _adminService.AddPromotion(model));

                try
                {
                    await _adminService.AddPromotion(model);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == InvalidPromotionIdFormat, GetErrorMsg(InvalidPromotionIdFormat, e.Message));
                }
            }
            else if (testCase == 3)
            {
                model.PromotionType = "author-10";

                Assert.ThrowsAsync<InvalidOperationException>(async () => await _adminService.AddPromotion(model));

                try
                {
                    await _adminService.AddPromotion(model);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == GenreAuthorNotFound, GetErrorMsg(GenreAuthorNotFound, e.Message));
                }
            }
            else
            {
                Assert.ThrowsAsync<ArgumentException>(async () => await _adminService.AddPromotion(model));

                try
                {
                    await _adminService.AddPromotion(model);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == ExistingPromotions, GetErrorMsg(ExistingPromotions, e.Message));
                }
            }
        }

        [TestCase(null, 3)]
        [TestCase("Alice", 1)]
        [TestCase("Doe", 1)]
        [TestCase("user2@example", 1)]
        [TestCase("Employee", 1)]
        public async Task GetUsers_ReturnsCorrectInfo(string searchTerm, int expectedCount)
        {
            await IdentityDatasetSeeder.IdentityData(context);

            var result = await _adminService.GetUsers(searchTerm);

            Assert.That(result.Count == expectedCount, GetErrorMsg(expectedCount, result.Count));
        }

        [TestCase("11111111-1111-1111-1111-111111111111", "33333333-3333-3333-3333-333333333333", "Employee")]
        [TestCase("11111111-1111-1111-1111-111111111111", "22222222-2222-2222-2222-222222222222", "Admin")]
        public async Task PromoteUser_AppliesNewRole(string adminId, string promoted, string expectedRole)
        {
            await IdentityDatasetSeeder.IdentityData(context);

            var administratorId = Guid.Parse(adminId);
            var promotedUserId = Guid.Parse(promoted);

            var result = await _adminService.PromoteUser(promotedUserId.ToString(), administratorId);

            Assert.IsTrue(result, GetErrorMsg(true, result));

            var dbContext = (ApplicationDbContext)context;
            var user = await dbContext.Users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .FirstAsync(u => u.Id == promotedUserId);

            Assert.That(user.UserRoles.Any(ur => ur.Role.Name == expectedRole));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task PromoteUser_ThrowsExceptions(int testCase)
        {
            await IdentityDatasetSeeder.IdentityData(context);
            var administratorId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var promotedUserId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var invalidUserId = "33333333-3333-3333-3333-333333333334";

            if (testCase == 1)
            {
                Assert.ThrowsAsync<ArgumentException>(async () => await _adminService.PromoteUser("", administratorId));

                try
                {
                    await _adminService.PromoteUser("", administratorId);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == InvalidUserIdFormat, GetErrorMsg(InvalidUserIdFormat, e.Message));
                }
            }
            else if (testCase == 2)
            {
                Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    await _adminService.PromoteUser(administratorId.ToString(), administratorId));

                try
                {
                    await _adminService.PromoteUser(administratorId.ToString(), administratorId);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == SelfPromotionError, GetErrorMsg(SelfPromotionError, e.Message));
                }
            }
            else if (testCase == 3)
            {
                Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    await _adminService.PromoteUser(invalidUserId, administratorId));

                try
                {
                    await _adminService.PromoteUser(invalidUserId, administratorId);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == UserNotFound, GetErrorMsg(UserNotFound, e.Message));
                }
            }
            else if (testCase == 4)
            {
                Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    await _adminService.PromoteUser(administratorId.ToString(), promotedUserId));

                try
                {
                    await _adminService.PromoteUser(administratorId.ToString(), promotedUserId);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == RoleOverflow, GetErrorMsg(RoleOverflow, e.Message));
                }
            }
        }

        [TestCase("11111111-1111-1111-1111-111111111111", "22222222-2222-2222-2222-222222222222", null)]
        [TestCase("33333333-3333-3333-3333-333333333333", "11111111-1111-1111-1111-111111111111", "Employee")]
        public async Task DemoteUser_AppliesNewRole(string adminId, string demoted, string expectedRole)
        {
            await IdentityDatasetSeeder.IdentityData(context);

            var administratorId = Guid.Parse(adminId);
            var demotedId = Guid.Parse(demoted);

            var result = await _adminService.DemoteUser(demotedId.ToString(), administratorId);

            Assert.IsTrue(result, GetErrorMsg(true, result));

            var dbContext = (ApplicationDbContext)context;
            var user = await dbContext.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstAsync(u => u.Id == demotedId);

            var resultRole = user.UserRoles.Select(r => r.Role.Name).FirstOrDefault(r => r == expectedRole);
            Assert.That(resultRole == expectedRole, GetErrorMsg(expectedRole, resultRole));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task DemoteUser_ThrowsExceptions(int testCase)
        {
            await IdentityDatasetSeeder.IdentityData(context);
            var administratorId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var demotedUserId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var invalidUserId = "33333333-3333-3333-3333-333333333334";

            if (testCase == 1)
            {
                Assert.ThrowsAsync<ArgumentException>(async () => await _adminService.DemoteUser("", administratorId));

                try
                {
                    await _adminService.DemoteUser("", administratorId);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == InvalidUserIdFormat, GetErrorMsg(InvalidUserIdFormat, e.Message));
                }
            }
            else if (testCase == 2)
            {
                Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    await _adminService.DemoteUser(administratorId.ToString(), administratorId));

                try
                {
                    await _adminService.DemoteUser(administratorId.ToString(), administratorId);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == SelfPromotionError, GetErrorMsg(SelfPromotionError, e.Message));
                }
            }
            else if (testCase == 3)
            {
                Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    await _adminService.DemoteUser(invalidUserId, administratorId));

                try
                {
                    await _adminService.DemoteUser(invalidUserId, administratorId);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == UserNotFound, GetErrorMsg(UserNotFound, e.Message));
                }
            }
            else if (testCase == 4)
            {
                Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    await _adminService.DemoteUser(demotedUserId.ToString(), administratorId));

                try
                {
                    await _adminService.DemoteUser(demotedUserId.ToString(), administratorId);
                }
                catch (Exception e)
                {
                    Assert.That(e.Message == RoleUnderflow, GetErrorMsg(RoleUnderflow, e.Message));
                }
            }
        }
    }
}
