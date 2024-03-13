using Microsoft.AspNetCore.Identity;

namespace WebShop.Core.Models.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {
            
        }

        public ApplicationRole(string role)
        :base(role)
        {
            
        }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
    }
}
