using OnlineShop_Data.Entities;
using OnlineShop_Data.IRepositories;

namespace OnlineShop_Data.EF.Repositories
{
    public class SlideRepository : EFRepository<Slide,int>,ISlideRepository
    {
        public SlideRepository(OnlineShopDbContext context) : base(context)
        {
        }
    }
}