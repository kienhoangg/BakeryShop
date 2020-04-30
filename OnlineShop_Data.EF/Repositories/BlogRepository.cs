using OnlineShop_Data.Entities;
using OnlineShop_Data.IRepositories;

namespace OnlineShop_Data.EF.Repositories
{
    public class BlogRepository : EFRepository<Blog,int> , IBlogRepository
    {
        public BlogRepository(OnlineShopDbContext context) : base(context)
        {
        }
    }
}