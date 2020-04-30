using OnlineShop_Data.Entities;
using OnlineShop_Data.IRepositories;

namespace OnlineShop_Data.EF.Repositories
{
    public class FooterRepository : EFRepository<Footer,string>, IFooterRepository
    {
        public FooterRepository(OnlineShopDbContext context) : base(context)
        {
        }
    }
}