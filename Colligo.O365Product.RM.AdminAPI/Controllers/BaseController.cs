using Colligo.O365Product.LoggerService;
using Colligo.O365Product.RM.Admin.Security.Extension;
using Colligo.O365Product.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Colligo.O365Product.RM.AdminAPI.Controllers
{
    [Authorize]
    public class BaseController : ApiController
    {
        protected string UserName { get; set; }

        protected string OrganizationId { get; set; }

        protected string ProductName { get; set; }

        protected readonly ILogManager _logger;

        public BaseController()
        {
            _logger = new ApplicationLog();
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                UserName = claimsIdentity.GetClaimValue(ClaimTypes.Email);
                OrganizationId = claimsIdentity.GetClaimValue("organizationName");
                ProductName = claimsIdentity.GetClaimValue("productName");
            }
        }
    }
}
