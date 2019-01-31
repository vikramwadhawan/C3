using Colligo.O365Product.RM.Admin.Security.Constant;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Admin.Security.Middleware
{
    public class OAuthAuthenticationMiddleware : OwinMiddleware
    {
        public OAuthAuthenticationMiddleware(OwinMiddleware next)
         : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            await Next.Invoke(context);
            if (context.Response.StatusCode == 400
                && context.Response.Headers.ContainsKey(
                          SecurityConstant.OwinChallengeFlag))
            {
                var headerValues = context.Response.Headers.GetValues
                      (SecurityConstant.OwinChallengeFlag);

                context.Response.StatusCode =
                       Convert.ToInt16(headerValues.FirstOrDefault());
                context.Response.Headers.Remove(
                       SecurityConstant.OwinChallengeFlag);
            }
        }
    }
}
