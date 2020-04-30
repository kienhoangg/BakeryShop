using OnlineShop_Data.Entities;
using OnlineShop_Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop_Data.EF.Repositories
{
    public class TagRepository : EFRepository<Tag, string>, ITagRepository
    {
        public TagRepository(OnlineShopDbContext context) : base(context)
        {
        }
    }
}
