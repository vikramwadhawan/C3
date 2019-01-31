using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Admin.Security.Extension
{
    public static class Extensions
    {
        public static string GetClaimValue(this ClaimsIdentity claimsIdentity, string type)
        {
            Claim userName = claimsIdentity.Claims.FirstOrDefault(p => p.Type == type);
            if (userName != null)
                return userName.Value;
            return null;
        }
    }
}
