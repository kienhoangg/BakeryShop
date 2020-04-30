using OnlineShop_Data.Entities;
using OnlineShop_Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop_Data.EF.Repositories
{
    public class ProductRepository : EFRepository<Product, int> , IProductRepository
    {
        public ProductRepository(OnlineShopDbContext context) : base(context)
        {
        }
    }
}
