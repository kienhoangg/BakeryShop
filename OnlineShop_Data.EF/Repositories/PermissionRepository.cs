using OnlineShop_Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using OnlineShop_Data.IRepositories;

namespace OnlineShop_Data.EF.Repositories
{
    public class PermissionRepository : EFRepository<Permission, int>, IPermissionRepository
    {
        public PermissionRepository(OnlineShopDbContext context) : base(context)
        {
        }
    }
}
