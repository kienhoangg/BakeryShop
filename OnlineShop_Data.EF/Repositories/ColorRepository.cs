using OnlineShop_Data.Entities;
using OnlineShop_Data.IRepositories;

namespace OnlineShop_Data.EF.Repositories
{
    public class ColorRepository : EFRepository<Color,int>,IColorRepository
    {
        public ColorRepository(OnlineShopDbContext context) : base(context)
        {
        }
    }
}