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
    public class EventSettingController : BaseController
    {
        [HttpGet]
        [Route("colligo/eventsetting/all")]
        public async Task<IEnumerable<EventSettingModel>> GetAllEventSetting()
        {
            try
            {
                return await EventSettingManager.GetAllEventSetting();
            }
            catch (Exception ex)
            {
                _logger.Error("GetAllEventSetting-- error- ", ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in GetAllEventSetting"));
            }
        }

        [HttpGet]
        [Route("colligo/eventsetting/mappingcount")]
        public async Task<IEnumerable<EventMappingModel>> GetEventSettingCount(string organizationUrl)
        {
            try
            {
                return await EventSettingManager.GetEventSettingCount( organizationUrl);
            }
            catch (Exception ex)
            {
                _logger.Error("GetEventSettingCount-- error- ", ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in GetEventSettingCount"));
            }
        }

        [HttpGet]
        [Route("colligo/eventsetting/byorgurl")]
        public async Task<IEnumerable<EventSettingModel>> GetAllEventSettingByOrganization(string organizationUrl)
        {
            try
            {
                return await EventSettingManager.GetAllEventSettingByOrganization(organizationUrl);
            }
            catch (Exception ex)
            {
                _logger.Error("GetAllEventSettingByOrganization-- error- ", ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in GetAllEventSettingByOrganization"));
            }
        }

        [HttpPost]
        [Route("colligo/eventsetting/save")]
        public async Task<string> SaveEventSetting(EventSettingModel eventSetup)
        {
            try
            {
                return await EventSettingManager.SaveEventSetting(eventSetup);
            }
            catch (Exception ex)
            {
                _logger.Error("SaveEventSetting-- error- ", ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in SaveEventSetting"));
            }
        }

        [HttpPost]
        [Route("colligo/eventsetting/update")]
        public async Task<string> UpdateEventSetting(long eventSettingId, int modifiedBy, bool isActive, DateTime? lastRetrivalTime = null)
        {
            try
            {
                return await EventSettingManager.UpdateEventSetting(eventSettingId, modifiedBy, lastRetrivalTime, isActive);
            }
            catch (Exception ex)
            {
                _logger.Error("UpdateEventSetting-- error- ", ex);
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Problem in UpdateEventSetting"));
            }
        }
    }
}
