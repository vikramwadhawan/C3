using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Colligo.O365Product.LoggerService;
using Colligo.O365Product.Model;
using Colligo.O365Product.RM.Model;
using Colligo.O365Product.ServiceInterface;
using Colligo.O365Product.SharePointData;
namespace Colligo.O365Product.SPServiceAPI.Controllers
{

    public class SPBrowseController : ApiController
    {
        private ILogManager _logger = new ApplicationLog();

        [HttpPost]
        [Route("spapi/contentlog/all")]
        public async Task<List<SpContentLogModel>> GetContentLog(ContentLogRequest request)
        {
            try
            {
                return await SharepointDataManager.GetModifiedContentInSharepoint(request);
            }
            catch (Exception ex)
            {
                _logger.Error("error in GetModifiedContents", ex);
            }
            return null;
        }

        [HttpPost]
        [Route("spapi/contentType/all")]
        public string GetAllContentTypesBySiteUrl(SharepointRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new Exception("Request cannot be null");
                }
                return SPBrowse.GetAllContentTypesBySiteUrl(request);
            }
            catch (Exception ex)
            {
                _logger.Error("GetAllContentTypesBySiteUrl- ", ex);
            }
            return "{}";
        }


        [HttpPost]
        [Route("spapi/fields/all")]
        public string GetAllFieldsByContentId(SharepointRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new Exception("Request cannot be null");
                }
                return SPBrowse.GetAllFieldsByContentId(request);
            }
            catch (Exception ex)
            {
                _logger.Error("GetAllFieldsByContentId--", ex);
            }
            return "{}";
        }

        [HttpPost]
        [Route("spapi/sites/all")]
        public string SearchSharepointSite(SharepointRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new Exception("Request cannot be null");
                }
                return SPBrowse.SearchSharepointSite(request);
            }
            catch (Exception ex)
            {
                _logger.Error("SearchSharepointSite--", ex);
            }
            return "{}";
        }
    }
}
