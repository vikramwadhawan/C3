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
    public class EventSettingUserController : BaseController
    {

        [HttpPost]
        [Route("colligo/eventsetting/saveuser")]
        public async Task<string> SaveUser(EventSettingUserModel organizationUser)
        {
            try
            {
                return await EventSettingUserManager.SaveEventSettingUser(organizationUser);
            }
            catch (Exception ex)
            {
                _logger.Error("SaveUser-- error- ", ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in SaveUser"));
            }
        }

        [HttpGet]
        [Route("colligo/eventsetting/user")]
        public async Task<EventSettingUserModel> GetUser(string  siteUrl)
        {
            try
            {
                return await EventSettingUserManager.GetEventSettingUser(siteUrl);
            }
            catch (Exception ex)
            {
                _logger.Error("GetUser-- error- ", ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in getiing user"));
            }
        }





    }
}
