namespace WebShop.Core.Repository
{
    using Data;
    using Contracts;
    public class OrdersRepository : Repository, IOrdersRepository
    {
        public OrdersRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
