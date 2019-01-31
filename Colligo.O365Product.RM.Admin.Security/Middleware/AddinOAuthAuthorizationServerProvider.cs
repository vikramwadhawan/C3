using Colligo.O365Product.RM.Admin.BLL;
using Colligo.O365Product.RM.Admin.Security.Constant;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Admin.Security.Middleware
{
    public class AddinOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            await base.ValidateClientAuthentication(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (string.IsNullOrWhiteSpace(context.UserName) || string.IsNullOrWhiteSpace(context.Password))
            {
                context.SetError(ErrorConstant.BlankCredential);
                context.Response.Headers.Add(SecurityConstant.OwinChallengeFlag, new[] { ((int)HttpStatusCode.Unauthorized).ToString() });
            }
            else
            {
                string productName = context.Request.Query.Get("product");
                string message = await UserManager.ValidateCredential(context.UserName, context.Password, productName);
                if (message.Contains("LoggedIn") || message.Contains("UserCreated"))
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, context.UserName));
                    claims.Add(new Claim(ClaimTypes.Email, context.UserName));
                    claims.Add(new Claim("organizationName", context.Password));
                    claims.Add(new Claim("productName", productName));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, context.UserName));
                    ClaimsIdentity oAuthIdentity = new ClaimsIdentity(claims, context.Options.AuthenticationType);
                    context.Validated(oAuthIdentity);
                }
                else if (message.Contains("LicenseFinished") || message.Contains("OrganizationNotRegistered")
                    || message.Contains("InActiveUser") || message.Contains("NoProductLicense"))
                {
                    context.SetError(ApplicationManager.GetErrorMessageByKey("LicenseFinished"));
                    //Add your flag to the header of the response
                    context.Response.Headers.Add(SecurityConstant.OwinChallengeFlag, new[] { ((int)HttpStatusCode.Unauthorized).ToString() });
                }
                else
                {
                    context.SetError(ErrorConstant.Invalidcredential);
                    //Add your flag to the header of the response
                    context.Response.Headers.Add(SecurityConstant.OwinChallengeFlag, new[] { ((int)HttpStatusCode.Unauthorized).ToString() });
                }
            }
            await base.GrantResourceOwnerCredentials(context);
        }
    }
}
