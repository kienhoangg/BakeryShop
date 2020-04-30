using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop_Application.Interfaces;

namespace OnlineShop.Controllers.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly IProductCategoryService _productCategoryService;

        public CategoryViewComponent(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View(_productCategoryService.GetAll());
        }

    }
}