using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.Models;
using OnlineShop_Data.Entities;
using OnlineShop_Utilities.Dtos;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger _logger;

        public LoginController(SignInManager<AppUser> signInManager, ILogger<LoginController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }
        public IActionResult Index(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Authen(LoginViewModel model, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                //var result = await _signInManager.PasswordSignInAsync(UserName, Password, false, lockoutOnFailure: false);
                if ( result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");  
                    return Json(new GenericResult(true,returnUrl));
                }
                if(result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return new OkObjectResult(new GenericResult(false, "User account locked out."));
                }
                else
                {
                    return new ObjectResult(new GenericResult(false, "Account is invalid !"));
                }
            }
            else
            {
                //var errors = ModelState.Select(x => x.Value.Errors)
                //          .Where(y => y.Count > 0)
                //          .ToList();
                return new ObjectResult(new GenericResult(false, "Notvalid"));
            }
            
        }
    }
}