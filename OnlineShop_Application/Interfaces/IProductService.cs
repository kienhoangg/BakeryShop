using OnlineShop_Application.ViewModels;
using OnlineShop_Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop_Application.Interfaces
{
    public interface IProductService : IDisposable
    {
        List<ProductViewModel> GetAll();

        PagedResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize);

        ProductViewModel Add(ProductViewModel productViewModel);

        void Update(ProductViewModel productViewModel);

        void Delete(int id);

        ProductViewModel GetById(int id);

        List<ProductViewModel> GetHotProduct(int top);

        List<ProductViewModel> GetLastest(int top); 
    }
}
