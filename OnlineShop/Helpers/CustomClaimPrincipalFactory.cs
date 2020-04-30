using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OnlineShop_Data.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShop.Helpers
{
    public class CustomClaimPrincipalFactory : UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public CustomClaimPrincipalFactory(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager, IOptions<IdentityOptions> options)
            : base (userManager,roleManager,options)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
            {
                new Claim("Email",user.Email),
                new Claim("FullName",user.FullName),
                new Claim("Avatar", user.Avatar ?? string.Empty),
                new Claim("Roles", string.Join(';',roles))
            }) ;
            return principal;
        }
    }
}
