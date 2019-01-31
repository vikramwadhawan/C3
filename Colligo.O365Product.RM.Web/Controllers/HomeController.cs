using Colligo.O365Product.Helper;
using Colligo.O365Product.Helper.Constant;
using Colligo.O365Product.Helper.Extensions;
using Colligo.O365Product.Helper.Helper;
using Colligo.O365Product.Model;
using Colligo.O365Product.RM.Model;
using Colligo.O365Product.Security.Helpers;
using Colligo.O365Product.Security.Model;
using Colligo.O365Product.ServiceInterface;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Colligo.O365Product.RM.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(ILogManager logManager, IAdminAPI adminApi) : base(logManager, adminApi)
        {
        }

        // GET: Home
        public async Task<ActionResult> Index()
        {
            ViewBag.Token = AdminAPIToken;
            if (string.IsNullOrEmpty(AdminAPIToken) || AdminAPIToken == GlobalConstants.INVALID_TOKEN)
            {
                _logger.Info("idToken is missing and forcing to CompleteLogout()");
                return CompleteLogout();
            }
            _adminApi.ApiToken = AdminAPIToken;
            _logger.Info($"Received idToken from session");
            string tenantUrl = TenantUrl;
            ViewBag.TenantUrl = tenantUrl;
            ViewBag.userEmail = CurrentUserName;
            UserModel userdetail = CurrentUserDetail;
            if (userdetail != null)
            {
                ViewBag.userId = userdetail.SystemUserId;
                ViewBag.orgMasterId = userdetail.OrganizationMasterId;
            }
            else
            {
                _logger.Info("user detail is missing and forcing to CompleteLogout()");
                return CompleteLogout();
            }
            try
            {
                ViewBag.SPTokensJson = "";
                var redirectUri = new Uri(Url.Action(nameof(LoginController.Authorize), "Login", null, Request.Url.Scheme));
                var authCode = AuthorizationCode;
                var resourceContexts = SpResourceContext;
                //in case tenanturl is null
                if (!string.IsNullOrEmpty(tenantUrl))
                {
                    Tuple<IList<SpResourceContext>, string> res = await ManageSharePointToken(tenantUrl, authCode, resourceContexts, redirectUri);
                    //get token for root site
                    if (res.Item2 == GlobalConstants.INVALID_GRANT)
                    {
                        return CompleteLogout();
                    };
                    if (res.Item1.HasItems())
                    {                       
                        SpResourceContext = res.Item1;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(Utility.LogStringFormat("HomeController", "Index()"), ex);
            }
            return View();
        }

        public ActionResult Configuration()
        {
            Response.ContentType = "application/javascript";
            return View();
        }

        public ActionResult CompleteLogout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            HttpCookie sessionCookie = System.Web.HttpContext.Current.Request.Cookies.Get("ASP.NET_SessionId");
            System.Web.HttpContext.Current.Request.Cookies.Clear();
            if (sessionCookie != null)
            {
                sessionCookie.Expires = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cookies.Add(sessionCookie);
            }
            System.Web.HttpContext.Current.Session.Abandon();
            return RedirectToAction("SignIn", "Login");
        }

        [Authorize]
        [HttpGet]
        public async Task<JsonResult> RefreshSharePointToken(string url)
        {
            // ResourceContext context = JsonHelper.ConvertToObject<ResourceContext>(tokenData);
            string freshTokenData = null;
            if (!string.IsNullOrEmpty(url))
            {
                //prepare data
                var redirectUri = new Uri(Url.Action(nameof(LoginController.Authorize), "Login", null, Request.Url.Scheme));
                var authCode = AuthorizationCode;
                var resourceContexts = SpResourceContext;
                Tuple<IList<SpResourceContext>, string> tokenResult = await ManageSharePointToken(url, authCode, resourceContexts, redirectUri);
                if (tokenResult.Item2 == GlobalConstants.INVALID_GRANT)
                {
                    return Json(new { data = GlobalConstants.INVALID_GRANT }, JsonRequestBehavior.AllowGet); ;
                };
                resourceContexts = tokenResult.Item1;
                SpResourceContext = resourceContexts;
                freshTokenData = GetTokenFromResource(url, resourceContexts);
            }
            return Json(new { data = freshTokenData }, JsonRequestBehavior.AllowGet);
        }

        private string GetTokenFromResource(string url, IList<SpResourceContext> resourceContext)
        {
            string token = string.Empty;
            if (resourceContext != null && resourceContext.Count > 0)
            {
                Uri spUrl = new Uri(url);
                var sameHost = resourceContext.FirstOrDefault(r => r.Host == spUrl.Host && !string.IsNullOrEmpty(r.AccessToken));

                //if sites exist then find refresh token
                if (sameHost != null)
                {
                    token = sameHost.AccessToken;
                }
            }
            return token;
        }

        private async Task<Tuple<IList<SpResourceContext>, string>> ManageSharePointToken(string siteUrl, string authCode, IList<SpResourceContext> resourceContexts, Uri redirectUri)
        {
            Uri spUrl = new Uri(siteUrl);
            Tuple<IList<SpResourceContext>, Token> tokenResult = null;
            Token spAccessToken = null;
            if (resourceContexts == null)
            {
                resourceContexts = new List<SpResourceContext>();
            }
            bool isError = false;
            try
            {
                tokenResult = await GetSpToken(siteUrl, authCode, resourceContexts, redirectUri);
                spAccessToken = tokenResult.Item2;
            }
            catch (AdalServiceException ex)
            {
                isError = true;
                _logger.Error(Utility.LogStringFormat("LocationController", "BrowseLocations()"), ex);
                if (ex.ErrorCode == GlobalConstants.INVALID_GRANT)
                {
                    return new Tuple<IList<SpResourceContext>, string>(resourceContexts, GlobalConstants.INVALID_GRANT);
                }
                else if (ex.ErrorCode == GlobalConstants.INVALID_RESOURCE)
                {
                    //show the resource on UI only as it is added by user
                    resourceContexts.Add(
                         new SpResourceContext
                         {
                             AccessToken = "",
                             Host = System.Web.HttpUtility.HtmlDecode(spUrl.Host),
                             Url = System.Web.HttpUtility.HtmlDecode(siteUrl)
                         });
                }
            }
            if (spAccessToken == null && !isError)
            {
                var sameSite = resourceContexts.Count(r => r.Url == siteUrl) > 0;
                if (!sameSite)
                {
                    SpResourceContext context = resourceContexts.FirstOrDefault(p => p.Host == spUrl.Host);
                    spAccessToken = new Token() { access_token = context.AccessToken, expires_on = context.ExpiresOn, refresh_token = context.RefreshToken };
                }
            }
            if (spAccessToken != null)
            {
                if (spAccessToken.error == GlobalConstants.INVALID_GRANT && string.IsNullOrEmpty(spAccessToken.suberror))
                {
                    return new Tuple<IList<SpResourceContext>, string>(resourceContexts, GlobalConstants.INVALID_GRANT);
                }
                //process the authentication result
                this.ProcessAuthResult(spAccessToken, siteUrl, spUrl, ref resourceContexts);
            }

            await Task.Delay(0);
            return new Tuple<IList<SpResourceContext>, string>(resourceContexts, string.Empty); ;
        }

        private async Task<Tuple<IList<SpResourceContext>, Token>> GetSpToken(string siteUrl, string authCode, IList<SpResourceContext> resourceContexts, Uri redirectUri)
        {
            _logger.Info($"Getting site from colligoapp as: {siteUrl}");
            Uri spUrl = new Uri(siteUrl);
            string resourceUrl = $"{spUrl.Scheme}://{spUrl.Host}";

            Token spAccessToken = null;
            if (resourceContexts != null && resourceContexts.Count > 0)
            {
                var sameHost = resourceContexts.Where(r => r.Host == spUrl.Host && !string.IsNullOrEmpty(r.AccessToken)).ToList();

                //if sites exist then find refresh token
                if (sameHost.HasItems())
                {
                    var siteInformation = sameHost.FirstOrDefault();
                    //get the time when it is expiring
                    if (siteInformation.ExpiresOn.HasValue())
                    {
                        DateTime expirationTime = Utility.UnixTimeStampToDateTime(Convert.ToDouble(siteInformation.ExpiresOn));
                        if (expirationTime <= DateTime.Now)
                        {
                            //get access token from refresh token
                            _logger.Info("Going to get a new token as previous one has expired");
                            string refreshTokenMeta = await TokenHelper.GetRefreshToken(resourceUrl, siteInformation.RefreshToken);
                            spAccessToken = JsonHelper.ConvertToObject<Token>(refreshTokenMeta);
                            siteInformation.AccessToken = spAccessToken.access_token;
                            siteInformation.RefreshToken = spAccessToken.refresh_token;
                            siteInformation.ExpiresOn = spAccessToken.expires_on;
                        }
                    }
                }
                else
                {
                    //for new sites
                    spAccessToken = JsonHelper.ConvertToObject<Token>(await TokenHelper.GetRefreshToken(resourceUrl, authCode));
                }
            }
            else
            {
                //for first time
                spAccessToken = JsonHelper.ConvertToObject<Token>(await TokenHelper.GetRefreshToken(resourceUrl, authCode));
            }
            return new Tuple<IList<SpResourceContext>, Token>(resourceContexts, spAccessToken);
        }

        private void ProcessAuthResult(Token authResult, string siteUrl, Uri spUrl, ref IList<SpResourceContext> resourceContexts)
        {
            var sameSite = resourceContexts.Count(r => r.Url == siteUrl) > 0;
            var sameHost = resourceContexts.Where(r => r.Host == spUrl.Host).ToList();

            //update token for all other host
            if (sameHost != null && sameHost.Count > 0)
            {
                foreach (var item in sameHost)
                {
                    CopyAccessToken(authResult.access_token, authResult.expires_on, authResult.refresh_token, item, siteUrl, spUrl.Host);
                }
            }

            if (!sameSite)
            {
                SpResourceContext context = new SpResourceContext();
                CopyAccessToken(authResult.access_token, authResult.expires_on, authResult.refresh_token, context, siteUrl, spUrl.Host);
                resourceContexts.Add(context);
            }


        }

        private void CopyAccessToken(string access_token, string expires_on, string refresh_token, SpResourceContext context, string siteUrl, string host)
        {
            if ((context.AccessToken != access_token) || context.AccessToken == null)
            {
                context.AccessToken = access_token;
                context.ExpiresOn = expires_on;
                context.RefreshToken = refresh_token;
                context.Host = System.Web.HttpUtility.HtmlDecode(host);
                context.Url = System.Web.HttpUtility.HtmlDecode(siteUrl);
            }
        }
    }
}