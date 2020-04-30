using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OnlineShop.Extensions;
using System.Security.Claims;
using System.Linq;
using OnlineShop_Application.Interfaces;
using OnlineShop_Application.ViewModels;
using System.Collections.Generic;

namespace OnlineShop.Areas.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly IFunctionService _functionService;
        public SideBarViewComponent(IFunctionService functionService)
        {
            _functionService = functionService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = ((ClaimsPrincipal)User).GetSpecifiedIdentity("Roles");
            var model = new List<FunctionViewModel>();
            if (roles.Split(';').Contains("Admin"))
            {
                model = await _functionService.GetAll();
            }
            return View(model);
        }
    }
}
