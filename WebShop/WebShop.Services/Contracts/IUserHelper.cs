using System.Security.Claims;

namespace WebShop.Services.Contracts
{
    public interface IUserHelper<TUser, TKey>
    {
        public abstract Task<TKey> GetUserId(ClaimsPrincipal user);
    }
}
