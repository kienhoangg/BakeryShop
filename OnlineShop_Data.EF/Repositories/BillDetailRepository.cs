using OnlineShop_Data.Entities;
using OnlineShop_Data.IRepositories;

namespace OnlineShop_Data.EF.Repositories
{
    public class BillDetailRepository : EFRepository<BillDetail,int>, IBillDetailRepository
    {
        public BillDetailRepository(OnlineShopDbContext context) : base(context)
        {
        }
    }
}