using OnlineShop_Data.Entities;
using OnlineShop_Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop_Data.EF.Repositories
{
    public class ProductCategoryRepository : EFRepository<ProductCategory, int>, IProductCategoryRepository
    {
        private readonly OnlineShopDbContext _context;
        public ProductCategoryRepository(OnlineShopDbContext context) : base(context)
        {
            _context = context;
        }

        public List<ProductCategory> GetByAlias(string alias)
        {
            return _context.ProductCategories.Where(x => x.SeoAlias == alias).ToList();
        }
    }
}
