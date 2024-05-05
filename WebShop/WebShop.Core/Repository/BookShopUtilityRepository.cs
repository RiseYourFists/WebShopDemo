namespace WebShop.Core.Repository
{
    using WebShop.Core.Contracts;
    using WebShop.Core.Data;

    public class BookShopUtilityRepository : Repository, IBookShopUtilityRepository
    {
        public BookShopUtilityRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
