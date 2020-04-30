using OnlineShop_Data.Entities;
using OnlineShop_Data.IRepositories;
using OnlineShop_Infrastructure.Interfaces;

namespace OnlineShop_Data.EF.Repositories
{
    public class BlogTagRepository : EFRepository<BlogTag,int>, IBlogTagRepository
    {
        public BlogTagRepository(OnlineShopDbContext context) : base(context)
        {
        }
    }
}