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
    public class ContentLogController : BaseController
    {
        [HttpPost]
        [Route("colligo/contentlog/save")]
        public async Task<string> SaveContentLog(List<SpContentLogModel> logs,long eventSettingId)
        {
            try
            {
                return await ContentLogManager.SaveContentLog(logs, eventSettingId);
            }
            catch (Exception ex)
            {
                _logger.Error("SaveContentLog-- error- ", ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in SaveContentLog"));
            }
        }
    }
}
