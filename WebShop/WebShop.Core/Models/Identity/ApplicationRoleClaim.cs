using Microsoft.AspNetCore.Identity;

namespace WebShop.Core.Models.Identity
{
    public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
    {
        public virtual ApplicationRole Role { get; set; }
    }
}
