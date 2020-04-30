using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop_Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
