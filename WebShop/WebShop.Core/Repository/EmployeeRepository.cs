using WebShop.Core.Data;

namespace WebShop.Core.Repository
{
    using Contracts;

    using Microsoft.EntityFrameworkCore;

    public class EmployeeRepository : Repository, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
