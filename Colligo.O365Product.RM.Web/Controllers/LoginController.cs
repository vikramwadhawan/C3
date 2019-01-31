using Colligo.O365Product.Helper.Constant;
using Colligo.O365Product.Helper.Helper;
using Colligo.O365Product.RM.Model;
using Colligo.O365Product.Security.Helpers;
using Colligo.O365Product.Security.Model;
using Colligo.O365Product.ServiceInterface;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Colligo.O365Product.RM.Web.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(ILogManager logManager, IAdminAPI adminApi) : base(logManager, adminApi)
        {
        }

        // The URL that auth should redirect to after a successful login.
        Uri loginRedirectUri => new Uri(Url.Action(nameof(Authorize), "Login", null, Request.Url.Scheme));

        // The URL to redirect to after a logout.
        Uri logoutRedirectUri => new Uri(Url.Action(nameof(HomeController.Index), "Login", null, Request.Url.Scheme));

        [AllowAnonymous]
        public async Task<ActionResult> SignIn()
        {
            if (!string.IsNullOrEmpty(AdminAPIToken))
            {
                return RedirectToAction("Index", "Home", new { frmLogin = "log" });
            }
            _logger.Info("Sigining In. Missing Id_token in session");
            string url = "";
            try
            {
                if (string.IsNullOrEmpty(Settings.AzureADClientId))
                {
                    TempData["ErrorLog"] = new AuthState()
                    {
                        message = "Please set your client ID  in the Web.config file"
                    };
                    return RedirectToActionPermanent("Error", "Login");
                }
                AuthState state = new AuthState()
                {
                    stateKey = Guid.NewGuid().ToString()
                };
                var queryParams = string.Format("&state={0}&nux=1&client-request-id={1}&x-client-SKU=Js&x-client-Ver=1.0.14&nonce={2}", JsonConvert.SerializeObject(state), Guid.NewGuid(), Guid.NewGuid());
                url = string.Concat(Settings.AzureADAuthority, "/oauth2/authorize?response_type=code+id_token&client_id=", Settings.AzureADClientId, "&redirect_uri=", loginRedirectUri, "&response_mode=form_post&prompt=login") + queryParams;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
            await Task.Delay(0);
            // Redirect the browser to the login page, then come back to the Authorize method below.
            return Redirect(url);
        }

        [AllowAnonymous]
        public async Task<ActionResult> Authorize(string id_token, string code, string state, string session_state, string error, string error_description)
        {
            _logger.Info("Authorization complete.");
            AuthState authState = null;
            if (state != null)
            {
                authState = JsonConvert.DeserializeObject<AuthState>(state);
            }
            if (authState == null)
                authState = new AuthState();
            var authContext = new AuthenticationContext(Settings.AzureADAuthority);
            try
            {
                string authCode = code;
                string idToken = id_token;
                if (!string.IsNullOrEmpty(code))
                {
                    _logger.Info("Got Id_token and authorization code");
                    Tuple<string, string> upnTid = JWTSecurityTokenHelper.ExtractUpnAndTidFromJwtToken(id_token);
                    if (upnTid != null)
                        CurrentUserName = upnTid.Item1;

                    Token apiOffice = await TokenHelper.GetAccessToken(authCode, loginRedirectUri.AbsoluteUri, Settings.GraphApiDiscovery);
                    if (apiOffice == null)
                    {
                        _logger.Info("failed to get token for graph api- forcing to failue authentication");
                        authState.authStatus = "failure";
                        authState.message = "Failed to get authenticate from O365";
                    }
                    else
                    {
                        var tenantUrl = await GraphDiscoveryApi.GetCurrentTenantSiteUrl(apiOffice);
                        GraphAPIToken = apiOffice;

                        string adminApiTokenDetails = _adminApi.GetAuthToken(idToken, Settings.ProductName);
                        // adminApiTokenDetails =
                        dynamic item = JsonHelper.ConvertToObject<object>(adminApiTokenDetails);

                        string adminApiToken = string.Empty;
                        string message = string.Empty;

                        if (item != null)
                        {
                            message = item["message"];
                            adminApiToken = item["status"];
                        }
                        if (string.IsNullOrEmpty(adminApiToken) || adminApiToken == GlobalConstants.INVALID_TOKEN)
                        {
                            _logger.Info("API_token is missing and forcing to failure");
                            authState.authStatus = "failure";
                            authState.message = message;
                        }
                        else
                        {
                            AdminAPIToken = adminApiToken;
                            AuthorizationCode = apiOffice.refresh_token;
                            TenantUrl = tenantUrl;
                            _adminApi.ApiToken = adminApiToken;
                            string userDetail = _adminApi.GetUserDetail(CurrentUserName);
                            if (userDetail != "fail")
                                CurrentUserDetail = JsonHelper.ConvertToObject<UserModel>(userDetail);
                            var claims = new List<Claim>();
                            claims.Add(new Claim("id_token", idToken));
                            var id = new ClaimsIdentity(claims, "ApplicationCookie");
                            HttpContext.GetOwinContext().Request.Headers.SetValues("Authorization", new string[] { "Bearer ", adminApiToken });
                            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, id);
                            HttpCookie authCookie = new HttpCookie("ASP.NET_SessionId", Session.SessionID);
                            authCookie.Expires = DateTime.Now.AddMonths(1);
                            Response.Cookies.Add(authCookie);
                            return RedirectToActionPermanent("Index", "Home", null);
                        }
                    }
                }
                else
                {
                    _logger.Error($"error during login from ad {error} - {error_description}");
                    authState.authStatus = "failure";
                    authState.message = "Failed to get authenticate from O365";
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                authState.authStatus = "failure";
                authState.message = "Technical error";
            }

            _logger.Info($"Authentication Status: {authState.authStatus}");
            await Task.FromResult(0);
            TempData["ErrorLog"] = authState;
            return RedirectToActionPermanent("Error", "Login");
        }

        public ActionResult Logout()
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
            return RedirectToAction("Index", "Login");
        }

        [AllowAnonymous]
        public ActionResult Error()
        {
            AuthState state = TempData["ErrorLog"] == null ? new AuthState() : (AuthState)TempData["ErrorLog"];
            return View(state);
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}