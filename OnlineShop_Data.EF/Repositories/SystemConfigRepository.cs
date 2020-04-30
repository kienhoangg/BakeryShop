using OnlineShop_Data.Entities;
using OnlineShop_Data.IRepositories;

namespace OnlineShop_Data.EF.Repositories
{
    public class SystemConfigRepository : EFRepository<SystemConfig,string> , ISystemConfigRepository
    {
        public SystemConfigRepository(OnlineShopDbContext context) : base(context)
        {
        }
    }
}