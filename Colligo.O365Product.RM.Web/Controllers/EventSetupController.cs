using Colligo.O365Product.Helper;
using Colligo.O365Product.Helper.Helper;
using Colligo.O365Product.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Colligo.O365Product.RM.Web.Controllers
{
    [Authorize]
    public class EventSetupController : BaseController
    {
        public EventSetupController(ILogManager logManager, IAdminAPI adminApi) : base(logManager, adminApi)
        {
        }

        [HttpGet]
        public JsonResult GetAllEventSetupByOrgUrl(string url)
        {
            try
            {
                string token = AdminAPIToken;
                if (!string.IsNullOrEmpty(token))
                {
                    string eventSetup = string.Empty;
                    _adminApi.ApiToken = token;
                    eventSetup = _adminApi.GetAllEventSettingByOrganization(url);
                    return Json(eventSetup, JsonRequestBehavior.AllowGet);
                }
                return Json("Request header is missing");
            }
            catch (Exception ex)
            {
                _logger.Error(Utility.LogStringFormat("EventSetupController", "GetAllEventSetupByOrgUrl()"), ex);
                return Json(string.Empty);
            }
        }

        [HttpGet]
        public JsonResult GetEventSettingCount(string url)
        {
            try
            {
                string token = AdminAPIToken;
                if (!string.IsNullOrEmpty(token))
                {
                    string eventSetup = string.Empty;
                    _adminApi.ApiToken = token;
                    eventSetup = _adminApi.GetEventSettingCount(url);
                    return Json(eventSetup, JsonRequestBehavior.AllowGet);
                }
                return Json("Request header is missing");
            }
            catch (Exception ex)
            {
                _logger.Error(Utility.LogStringFormat("EventSetupController", "GetEventSettingCount()"), ex);
                return Json(string.Empty);
            }
        }

        [HttpPost]
        public JsonResult SaveEventSetup(string setup)
        {
            try
            {
                string token = AdminAPIToken;
                if (!string.IsNullOrEmpty(token))
                {
                    string result = string.Empty;
                    _adminApi.ApiToken = token;
                    result = _adminApi.SaveEventSetting(setup);
                    return Json(result, JsonRequestBehavior.DenyGet);
                }
                return Json("Request header is missing");
            }
            catch (Exception ex)
            {
                _logger.Error(Utility.LogStringFormat("EventSetupController", "SaveEventSetup()"), ex);
                return Json(string.Empty);
            }
        }

        [HttpPost]
        public JsonResult UpdateEventSetup(string setup)
        {
            try
            {
                string token = AdminAPIToken;
                if (!string.IsNullOrEmpty(token))
                {
                    string result = string.Empty;
                    _adminApi.ApiToken = token;
                    result = _adminApi.UpdateEventSetting(setup);
                    return Json(result, JsonRequestBehavior.DenyGet);
                }
                return Json("Request header is missing");
            }
            catch (Exception ex)
            {
                _logger.Error(Utility.LogStringFormat("EventSetupController", "SaveEventSetup()"), ex);
                return Json(string.Empty);
            }
        }
    }
}