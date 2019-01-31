using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Colligo.O365Product.Security.Helpers
{
    public class Settings
    {
        public static string AzureADClientId = ConfigurationManager.AppSettings["ida:ClientId"];
        public static string AzureADClientSecret = ConfigurationManager.AppSettings["ida:appKey"];
        public static string AzureADAuthority = ConfigurationManager.AppSettings["ida:aadInstance"];
        public static string AzureADLogoutAuthority = @"https://login.microsoftonline.com/common/oauth2/logout?post_logout_redirect_uri=";       
        public static string GraphApiDiscovery = "https://graph.microsoft.com/";
        public static string GraphApiDiscoveryPath = ConfigurationManager.AppSettings["graphApiDiscoveryPath"];
        public static string ProductName = ConfigurationManager.AppSettings["product"];
        /// <summary>
        /// Ensures that the current key to the SessionToken table is in the cookie.
        /// </summary>
        /// <param name="ctx">The HTTP request context.</param>
        /// <returns></returns>
        public static string GetUserAuthStateId(HttpContextBase ctx)
        {
            string id;
            if (ctx.Request.Cookies[SessionKeys.Login.UserAuthStateId] == null)
            {
                // Convert GUID to a string and format as numeral to remove hyphens.
                id = Guid.NewGuid().ToString("N");
                ctx.Response.Cookies.Add(new HttpCookie(SessionKeys.Login.UserAuthStateId)
                {
                    Expires = DateTime.Now.AddMinutes(20),
                    Value = id
                });
            }
            else
            {
                id = ctx.Request.Cookies[SessionKeys.Login.UserAuthStateId].Value;
            }

            return id;
        }
    }
}
