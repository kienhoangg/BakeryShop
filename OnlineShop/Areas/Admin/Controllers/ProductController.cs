using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineShop_Application.Interfaces;
using OnlineShop_Application.ViewModels;
using OnlineShop_Utilities.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var model = _productService.GetAll();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public IActionResult GetAllProductByPagging(int? categoryId, string keyword, int page = 1, int pageSize = 10)
        {
            var model = _productService.GetAllPaging(categoryId, keyword, page, pageSize);
            return new OkObjectResult(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return new OkObjectResult(true);
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _productService.GetById(id);
            return new OkObjectResult(model);
        }
        [HttpPost]
        public IActionResult SaveEntity(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                string seoAlias = TextHelper.ToUnsignString(productViewModel.Name);
                productViewModel.SeoAlias = seoAlias;
                if (productViewModel.Id == 0)
                {
                    _productService.Add(productViewModel);
                    return new OkObjectResult(true);
                }
                else
                {
                    _productService.Update(productViewModel);
                    return new OkObjectResult(true);
                }
            }
            else
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
        }
    }
}