
namespace WebShop.Core.Repository
{
    using Data;

    using Contracts;
    public class AdminRepository : Repository, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
