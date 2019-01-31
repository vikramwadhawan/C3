using Colligo.O365Product.RM.Admin.BLL;
using Colligo.O365Product.RM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Colligo.O365Product.RM.AdminAPI.Controllers
{  
    public class UserController : BaseController
    {
        [HttpGet]
        [Route("colligo/user/detail")]
        public async Task<UserModel> GetUserDetailByUserId(string userEmail)
        {
            try
            {
                return await UserManager.GetUserDetailByUserId(userEmail);
            }
            catch (Exception ex)
            {
                _logger.Error("GetUserDetailByUserId-- error- ", ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in GetUserDetailByUserId"));
            }
        }
    }
}
