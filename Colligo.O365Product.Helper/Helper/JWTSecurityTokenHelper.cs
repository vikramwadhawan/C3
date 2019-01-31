using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Helper.Helper
{
    public static class JWTSecurityTokenHelper
    {
        public static string GetClaimValue(this IEnumerable<Claim> claimsIdentity, string type)
        {
            Claim userName = claimsIdentity.FirstOrDefault(p => p.Type == type);
            if (userName != null)
                return userName.Value;
            return null;
        }

        public static Tuple<string, string> ExtractUpnAndTidFromJwtToken(string jwtToken)
        {
            Tuple<string, string> result = null;
            JwtSecurityToken jst = new JwtSecurityToken(jwtToken);
            if (jst != null)
            {
                if (jst.Payload != null)
                {
                    List<Claim> claims = jst.Payload.Claims.ToList();
                    if (claims != null && claims.Count > 0)
                    {
                        string userName = claims.GetClaimValue("upn");
                        string tenantId = claims.GetClaimValue("tid");
                        result = new Tuple<string, string>(userName, tenantId);
                    }
                }
            }
            return result;
        }
    }
}
