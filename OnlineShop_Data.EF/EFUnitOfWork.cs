using OnlineShop_Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop_Data.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly OnlineShopDbContext _context;
        public EFUnitOfWork(OnlineShopDbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if(_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
