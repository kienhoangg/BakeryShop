using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineShop_Application.Interfaces;
using OnlineShop_Application.ViewModels;
using OnlineShop_Utilities.Dtos;
using OnlineShop_Utilities.Helpers;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        private readonly IProductCategoryService _productCategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _productCategoryService.GetAll();
            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult ReOrder(int sourceId, int targetId, Dictionary<int, int> lstChildren)
        {

            if (sourceId == targetId)
            {
                return new BadRequestResult();
            }
            if (lstChildren.Count == 0)
            {
                _productCategoryService.ReOrder(sourceId, targetId);
                return new OkObjectResult(new GenericResult(true));
            }
            else
            {
                _productCategoryService.UpdateParentId(sourceId, targetId, lstChildren);
                return new OkObjectResult(new GenericResult(true));
            }
        }

        public IActionResult SaveEntity(ProductCategoryViewModel productCategoryViewModel)
        {
            if(ModelState.IsValid)
            {
                string seoAlias = TextHelper.ToUnsignString(productCategoryViewModel.Name);
                productCategoryViewModel.SeoAlias = seoAlias;
                if(productCategoryViewModel.Id == 0)
                {
                    _productCategoryService.Add(productCategoryViewModel);
                    return new OkObjectResult(true);
                }
                else
                {
                    _productCategoryService.Update(productCategoryViewModel);
                    return new OkObjectResult(true);
                }
            }
            else
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
        }
        [HttpPost]
        public IActionResult UpdateParentId(int sourceId, int targetId, Dictionary<int, int> lstChildren)
        {
            //if (!ModelState.IsValid)
            //{
            //    var errors = ModelState.Select(x => x.Value.Errors)
            //              .Where(y => y.Count > 0)
            //              .ToList();
            //    return new BadRequestObjectResult(ModelState);
            //}
            //else
            //{
            if (sourceId == targetId)
            {
                return new BadRequestResult();
            }

            _productCategoryService.UpdateParentId(sourceId, targetId, lstChildren);
            return new OkObjectResult(new GenericResult(true));
            //}
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _productCategoryService.Delete(id);
            return new OkObjectResult(true);
        }
        [HttpPost]
        public IActionResult GetById(int id)
        {
            var model = _productCategoryService.GetById(id);
            return new OkObjectResult(model);
        }
    }
}