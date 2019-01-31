using Colligo.O365Product.Helper;
using Colligo.O365Product.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Colligo.O365Product.Helper.Helper;

namespace Colligo.O365Product.RM.Web.Controllers
{
    [Authorize]
    public class EventSettingUserController : BaseController
    {

        public EventSettingUserController(ILogManager logManager, IAdminAPI adminApi) : base(logManager, adminApi)
        {
        }

        // GET: Organization
        [HttpPost]
        public async Task<JsonResult> SaveUser(string data)
        {
            string apitoken = AdminAPIToken;
            try
            {
                if (!string.IsNullOrEmpty(apitoken))
                {
                    string result = await _adminApi.SaveEventSettingUser(data, apitoken);
                    return Json(result, JsonRequestBehavior.DenyGet);
                }
                return Json("Request toekn is missing", JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                _logger.Error(Utility.LogStringFormat("EventSettingUser Controller", "SaveUser()"), ex);
                return Json(string.Empty, JsonRequestBehavior.DenyGet);
            }
        }

        // GET: Organization
        public async Task<JsonResult> GetUser(string siteUrl)
        {
            string apitoken = AdminAPIToken;
            try
            {

                if (!string.IsNullOrEmpty(apitoken))
                {
                    string result = await _adminApi.GetEventSettingUser(siteUrl, apitoken);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json("Request toekn is missing", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.Error(Utility.LogStringFormat("EventSettingUser Controller", "SaveUser()"), ex);
                return Json(string.Empty);
            }
        }

        public ActionResult Configuration()
        {
            Response.ContentType = "application/javascript";
            return View();
        }

        public JsonResult GetTimeZone()
        {
            var list = TimeZoneInfo.GetSystemTimeZones();
            string jsonData = JsonHelper.ConvertToJson(list.Select(p => new { id = p.Id, displayName = p.DisplayName }));
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}