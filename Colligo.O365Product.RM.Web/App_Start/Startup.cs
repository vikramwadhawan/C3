using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(Colligo.O365Product.RM.Web.App_Start.Startup))]

namespace Colligo.O365Product.RM.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            ConfigWebAuth(app);
        }

        private static void ConfigWebAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                AuthenticationMode = AuthenticationMode.Active,
                CookieName = "ColligoRMOApp",
                SlidingExpiration = true,
                ExpireTimeSpan = TimeSpan.FromDays(7.0),
                LoginPath = PathString.FromUriComponent("/Login/Index")
            });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {
                //AccessTokenFormat = new JwtFormat(new TokenValidationParameters
                //{
                //    // This is where you specify that your API only accepts tokens from its own clients
                //    ValidAudience = Settings.AzureADClientId,
                //    AuthenticationType = "oidc",
                //     ValidateIssuer=false

                //}, new OpenIdConnectCachingSecurityTokenProvider(Settings.AzureADAuthority))

            });
        }
    }
}
