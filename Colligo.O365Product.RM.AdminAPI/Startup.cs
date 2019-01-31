using System;
using System.Threading.Tasks;
using Colligo.O365Product.RM.Admin.Security.Middleware;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(Colligo.O365Product.RM.AdminAPI.Startup))]

namespace Colligo.O365Product.RM.AdminAPI
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static OAuthBearerAuthenticationOptions OAuthBOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            #region Oauth 2.0 Config Section

            app.Use<OAuthAuthenticationMiddleware>();

            OAuthOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/colligoproduct/token"),
                Provider = new AddinOAuthAuthorizationServerProvider { },
#if DEBUG
                AllowInsecureHttp = true,
#else
                AllowInsecureHttp = false,
#endif
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(30),
                AuthenticationMode = AuthenticationMode.Active
            };

            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            #endregion
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
