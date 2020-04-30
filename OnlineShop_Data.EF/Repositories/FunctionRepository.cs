using OnlineShop_Data.Entities;
using OnlineShop_Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop_Data.EF.Repositories
{
    public class FunctionRepository : EFRepository<Function, string>, IFunctionRepository
    {
        public FunctionRepository(OnlineShopDbContext context) : base(context)
        {
        }
    }
}
