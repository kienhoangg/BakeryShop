using OnlineShop_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop_Application.Interfaces
{
    public interface IFunctionService : IDisposable
    {
        Task<List<FunctionViewModel>> GetAll();
        void Update(FunctionViewModel functionVm);
        void Add(FunctionViewModel functionVm);

    }
}
