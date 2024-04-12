namespace WebShop.Testing.DummyClasses
{
    using Moq;
    using System.Security.Claims;
    using Core.Models.Identity;
    using Services.ServiceControllers;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    public static class UserHelperMockSetup
    {
        private static readonly Mock<IUserStore<ApplicationUser>> MockUserStore = new();
        private static readonly Mock<HttpContextAccessor> MockHttpContextAccessor = new();

        public static Mock<UserManager<ApplicationUser>> UserManagerMock =
            new(MockUserStore.Object,
                 null,
                 null,
                 null,
                 null,
                 null,
                 null,
                 null,
                 null);

        public static Mock<SignInManager<ApplicationUser>> SignInManagerMock =
            new(UserManagerMock.Object,
                 MockHttpContextAccessor.Object,
                 Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                 null,
                 null,
                 null,
                 null);

        public static Mock<UserHelper<ApplicationUser, Guid>> ConfigureMock(this Mock<UserHelper<ApplicationUser, Guid>> mock)
        {
            mock.Setup(m => m.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(Guid.Parse("3f7f6b82-527b-4e4b-bb58-ace1a0c7a281"));
            return mock;
        }
    }
}
