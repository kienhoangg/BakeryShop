using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShop.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetSpecifiedIdentity(this ClaimsPrincipal claimsPrincipal, string type)
        {
            var claim = claimsPrincipal.Claims.Where(x => x.Type == type).FirstOrDefault();
            string value = claim.Value;
            return value != null ? value : string.Empty;

        }
    }
}
