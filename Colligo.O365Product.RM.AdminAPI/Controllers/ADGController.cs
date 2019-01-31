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
    public class ADGController : BaseController
    {
        [HttpGet]
        [Route("colligo/adg/all")]
        public async Task<IEnumerable<ADGAgentModel>> GetADGActivity()
        {
            try
            {
                return await ADGManager.GetADGActivity();
            }
            catch (Exception ex)
            {
                _logger.Error("GetADGActivity-- error- ", ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in GetADGActivity"));
            }
        }

        [HttpPost]
        [Route("colligo/adg/update")]
        public async Task<string> SaveADGAgent(ADGAgentModel model)
        {
            try
            {
                return await ADGManager.SaveADGAgent(model);
            }
            catch (Exception ex)
            {
                _logger.Error("SaveContentLog-- error- ", ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in SaveContentLog"));
            }
        }

    }
}
