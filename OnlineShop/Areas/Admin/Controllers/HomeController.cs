using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Extensions;

namespace OnlineShop.Areas.Admin.Controllers
{

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var email = User.GetSpecifiedIdentity("Email");
            return View();
        }
    }
}