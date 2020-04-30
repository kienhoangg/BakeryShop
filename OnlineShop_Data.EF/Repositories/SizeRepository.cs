using OnlineShop_Data.Entities;
using OnlineShop_Data.IRepositories;

namespace OnlineShop_Data.EF.Repositories
{
    public class SizeRepository: EFRepository<Size,int>, ISizeRepository
    {
        public SizeRepository(OnlineShopDbContext context) : base(context)
        {
        }
    }
}