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
    [Authorize]
    public class DashboardController : BaseController
    {
        [HttpPost]
        [Route("colligo/dashboard/search")]
        public async Task<EventReportModel> GetEventReport(DashboardSearchModel searchModel)
        {
            try
            {
                return await DashboardManager.GetEventReport(searchModel);
            }
            catch (Exception ex)
            {
                _logger.Error("GetEventReport-- error- ", ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in GetEventReport"));
            }
        }

        [HttpPost]
        [Route("colligo/dashboard/contentlog")]
        public async Task<IEnumerable<DashboardContentModel>> GetContentLog(DashboardSearchModel searchModel)
        {
            try
            {
                return await DashboardManager.GetContentLog(searchModel);
            }
            catch (Exception ex)
            {
                _logger.Error("GetContentLog-- error- ", ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in GetContentLog"));
            }
        }

        [HttpPost]
        [Route("colligo/dashboard/eventsummary")]
        public async Task<IEnumerable<EventSummaryModel>> GetEventSummary(DashboardSearchModel searchModel)
        {
            try
            {
                return await DashboardManager.GetEventSummary(searchModel);
            }
            catch (Exception ex)
            {
                _logger.Error("GetEventSummary-- error- ", ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in GetEventSummary"));
            }
        }

        [HttpPost]
        [Route("colligo/dashboard/contentaudit")]
        public async Task<IEnumerable<ContentLogSummaryModel>> GetContentAuditSummary(DashboardSearchModel searchModel)
        {
            try
            {
                return await DashboardManager.GetContentAuditSummary(searchModel);
            }
            catch (Exception ex)
            {
                _logger.Error("GetContentAuditSummary-- error- ", ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in GetContentAuditSummary"));
            }
        }
    }
}
