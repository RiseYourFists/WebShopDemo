namespace WebShop.Testing.DummyClasses
{
    using System.Security.Claims;

    using Core.Models.Identity;
    using Services.Contracts;
    public class UserHelperMock : IUserHelper<ApplicationUser, Guid>
    {
        public async Task<Guid> GetUserId(ClaimsPrincipal user)
        {
            var id = Guid.Parse("3f7f6b82-527b-4e4b-bb58-ace1a0c7a281");
            return await Task.FromResult(id);
        }
    }
}
