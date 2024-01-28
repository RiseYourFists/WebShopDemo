
namespace WebShop.Core.Repository
{
    using Data;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    public class BookShopRepository : Repository, IBookShopRepository
    {
        public BookShopRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
