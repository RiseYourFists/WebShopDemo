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
            else if(testCase == 3)
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
            else if(testCase == 4)
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
    }
}
