namespace WebShop.Services.ServiceControllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Identity;
    using WebShop.Core.Contracts;

    /// <summary>
    /// Helper class for simplified use of commonly used methods.
    /// </summary>
    /// <typeparam name="TUser">IdentityUser primary key. The type is set to string by default.</typeparam>
    /// <typeparam name="TKey">Primary key type</typeparam>
    public class UserHelper<TUser, TKey>
        where TUser : class, IUserIdentity<TKey>
    {
        public UserHelper(
            UserManager<TUser> userManager,
            SignInManager<TUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        /// <summary>
        /// Additional access to UserManager.
        /// </summary>
        public UserManager<TUser> UserManager { get; set; }

        /// <summary>
        /// Additional access to SignInManager.
        /// </summary>
        public SignInManager<TUser> SignInManager { get; set; }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        /// <typeparam name="TKey">The primary key type of the IdentityUser that's used for the current instance. For example if IdentityUser&lt;Guid&gt; then TKey has to be &lt;Guid&gt;.</typeparam>
        /// <param name="user">Targeted user.</param>
        /// <returns>Task&lt;TKey&gt;</returns>
        public virtual async Task<TKey> GetUserId(ClaimsPrincipal user)
        {
            var userQuery = await UserManager.GetUserAsync(user);
            return (TKey)userQuery.Id;
        }
    }
}
