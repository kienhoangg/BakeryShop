using OnlineShop_Data.Entities;
using OnlineShop_Data.IRepositories;

namespace OnlineShop_Data.EF.Repositories
{
    public class BillRepository : EFRepository<Bill, int>, IBillRepository
    {
        public BillRepository(OnlineShopDbContext context) : base(context)
        {
        }
    }
}