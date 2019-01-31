using Colligo.O365Product.Model;
using Colligo.O365Product.RM.Model;
using Colligo.O365Product.Security.Model;
using Colligo.O365Product.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Colligo.O365Product.RM.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogManager _logger;
        protected IAdminAPI _adminApi;

        public BaseController(ILogManager logger, IAdminAPI adminApi)
        {
            _logger = logger;
            _adminApi = adminApi;
        }

        protected string CurrentUserName
        {
            get
            {
                return Convert.ToString(System.Web.HttpContext.Current.Session["CurrentUserName"]);
            }
            set
            {
                System.Web.HttpContext.Current.Session["CurrentUserName"] = value;
            }
        }

        protected UserModel CurrentUserDetail
        {
            get
            {
                return (UserModel)System.Web.HttpContext.Current.Session["CurrentUserDetail"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["CurrentUserDetail"] = value;
            }
        }

        protected Token GraphAPIToken
        {
            get
            {
                return (Token)System.Web.HttpContext.Current.Session["api_graph"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["api_graph"] = value;
            }
        }

        protected IList<SpResourceContext> SpResourceContext
        {
            get
            {
                return (IList<SpResourceContext>)System.Web.HttpContext.Current.Session["resource_contexts"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["resource_contexts"] = value;
            }
        }

        protected string AdminAPIToken
        {
            get
            {
                return Convert.ToString(System.Web.HttpContext.Current.Session["admin_token"]);
            }
            set
            {
                System.Web.HttpContext.Current.Session["admin_token"] = value;
            }
        }

        protected string TenantUrl
        {
            get
            {
                return Convert.ToString(System.Web.HttpContext.Current.Session["tenant_url"]);
            }
            set
            {
                System.Web.HttpContext.Current.Session["tenant_url"] = value;
            }
        }

        protected string AuthorizationCode
        {
            get
            {
                return Convert.ToString(System.Web.HttpContext.Current.Session["auth_code"]);
            }
            set
            {
                System.Web.HttpContext.Current.Session["auth_code"] = value;
            }
        }
    }
}