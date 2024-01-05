using Microsoft.AspNetCore.Identity;

namespace WebShop.Core.Models.Identity
{
    public class ApplicationUserToken : IdentityUserToken<Guid>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
