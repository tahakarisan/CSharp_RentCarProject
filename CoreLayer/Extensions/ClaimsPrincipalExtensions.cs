using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimPrincipal,string claimType)
        {
            var result = claimPrincipal.FindAll(claimType).Select(x=>x.Value).ToList();
            return result;
        }
        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
          
            return claimsPrincipal.Claims(ClaimTypes.Role);
        }
    }
}
