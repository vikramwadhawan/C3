using Colligo.O365Product.Helper;
using Colligo.O365Product.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Colligo.O365Product.RM.Web.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        public DashboardController(ILogManager logManager, IAdminAPI adminApi) : base(logManager, adminApi)
        {
        }


        [HttpPost]
        public JsonResult SearchEventReport(string criteria)
        {
            try
            {
                string token = AdminAPIToken;
                if (!string.IsNullOrEmpty(token))
                {
                    string result = string.Empty;
                    _adminApi.ApiToken = token;
                    result = _adminApi.SearchDashboardReport(criteria);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json("Request header is missing");
            }
            catch (Exception ex)
            {
                _logger.Error(Utility.LogStringFormat("DashboardController", "SearchEventReport()"), ex);
                return Json(string.Empty);
            }
        }

        [HttpPost]
        public JsonResult GetAllDashboardContentLog(string criteria)
        {
            try
            {
                string token = AdminAPIToken;
                if (!string.IsNullOrEmpty(token))
                {
                    string result = string.Empty;
                    _adminApi.ApiToken = token;
                    result = _adminApi.GetAllDashboardContentLog(criteria);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json("Request header is missing");
            }
            catch (Exception ex)
            {
                _logger.Error(Utility.LogStringFormat("DashboardController", "GetAllDashboardContentLog()"), ex);
                return Json(string.Empty);
            }
        }

        [HttpPost]
        public JsonResult GetEventSummary(string criteria)
        {
            try
            {
                string token = AdminAPIToken;
                if (!string.IsNullOrEmpty(token))
                {
                    string result = string.Empty;
                    _adminApi.ApiToken = token;
                    result = _adminApi.GetEventSummary(criteria);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json("Request header is missing");
            }
            catch (Exception ex)
            {
                _logger.Error(Utility.LogStringFormat("DashboardController", "GetEventSummary()"), ex);
                return Json(string.Empty);
            }
        }

        [HttpPost]
        public JsonResult GetContentAuditSummary(string criteria)
        {
            try
            {
                string token = AdminAPIToken;
                if (!string.IsNullOrEmpty(token))
                {
                    string result = string.Empty;
                    _adminApi.ApiToken = token;
                    result = _adminApi.GetContentAuditSummary(criteria);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json("Request header is missing");
            }
            catch (Exception ex)
            {
                _logger.Error(Utility.LogStringFormat("DashboardController", "GetContentAuditSummary()"), ex);
                return Json(string.Empty);
            }
        }
    }
}