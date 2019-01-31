using Colligo.O365Product.Helper.Helper;
using Colligo.O365Product.RM.Admin.DAL;
using Colligo.O365Product.RM.Admin.Data;
using Colligo.O365Product.RM.Admin.Data.Constant;
using Colligo.O365Product.RM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Admin.BLL
{
    public class EventSettingManager
    {
        /// <summary>
        /// get eventsetting models
        /// </summary>
        /// <param name="notCompleted"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<EventSettingModel>> GetAllEventSetting(bool isActive = true)
        {
            List<EventSettingModel> activities = new List<EventSettingModel>();
            var result = await ColligoO365RMOManager<VwEventSetting>.FindAsync(x => x.IsActive == isActive && x.UserIsActive, false);
            //converT data to framework model
            if (result != null && result.Any())
            {
                Type source = typeof(VwEventSetting);
                Type destination = typeof(EventSettingModel);
                foreach (var item in result)
                {
                    EventSettingModel obj = new EventSettingModel();
                    CopyHelper.Copy(source, item, destination, obj);
                    activities.Add(obj);
                }
            }
            return activities;
        }

        /// <summary>
        /// get eventsetting models
        /// </summary>
        /// <param name="notCompleted"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<EventSettingModel>> GetAllEventSettingByOrganization(string organizationUrl, bool includeAll = true)
        {
            List<EventSettingModel> activities = new List<EventSettingModel>();
            var result = await ColligoO365RMOManager<VwEventSetting>.FindAsync(x => x.ContentSiteUrl.ToLower() == organizationUrl.ToLower() && x.UserIsActive && (x.IsActive || includeAll), false);
            //converT data to framework model
            if (result != null && result.Any())
            {
                Type source = typeof(VwEventSetting);
                Type destination = typeof(EventSettingModel);
                foreach (var item in result)
                {
                    EventSettingModel obj = new EventSettingModel();
                    CopyHelper.Copy(source, item, destination, obj);
                    activities.Add(obj);
                }
            }
            return activities;
        }


        public static async Task<string> UpdateEventSetting(long eventSettingId, int modifiedBy, DateTime? lastRetrivalTime, bool isActive)
        {
            string result = "success";
            EventSetting setting = await ColligoO365RMOManager<EventSetting>.FirstOrDefaultAsync(p => p.EventSettingId == eventSettingId);
            //update settings with last execution date
            if (setting != null)
            {
                setting.LastRetrivalTime = lastRetrivalTime;
                setting.IsActive = isActive;
                setting.ModifiedBy = modifiedBy;
                setting.ModifiedOn = DateTime.Now.ToUniversalTime();
                int count = await ColligoO365RMOManager<EventSetting>.SaveAsync(null, setting);
                if (count == 0)
                    result = "fail";
            }
            return result;
        }

        public static async Task<string> SaveEventSetting(EventSettingModel eventSetup)
        {
            EventSetting setting = null;
            if (eventSetup.EventSettingId > 0)
                setting = await ColligoO365RMOManager<EventSetting>.FirstOrDefaultAsync(p => p.EventSettingId == eventSetup.EventSettingId);
            if (setting == null)
                setting = new EventSetting() { CreatedOn = DateTime.Now };
            Type source = typeof(EventSettingModel);
            Type destination = typeof(EventSetting);
            List<string> excludeProperties = new List<string>() { "CreatedOn" };
            CopyHelper.Copy(source, eventSetup, destination, setting, excludeProperties);
            string result = "success";
            int count = 0;
            //update settings with last execution date
            if (setting.EventSettingId > 0)
            {
                setting.ModifiedBy = eventSetup.CreatedBy;
                setting.ModifiedOn = DateTime.Now;
                count = await ColligoO365RMOManager<EventSetting>.SaveAsync(null, setting);
            }
            else
            {
                count = await ColligoO365RMOManager<EventSetting>.SaveAsync(setting);
            }
            if (count == 0)
                result = "fail";
            return result;
        }

        public static async Task<IEnumerable<EventMappingModel>> GetEventSettingCount(string organizationUrl)
        {
            List<EventMappingModel> modelResult = new List<EventMappingModel>();
            EventMappingModel report = null;
            var result = await ColligoO365RMOManager<EventSettingCount>.ExecuteSqlQueryAsync(Procedure.UspEventSettingGetMappingCount,new object[] { organizationUrl });
            //converT data to framework model
            if (result != null && result.Any())
            {
                Type source = typeof(EventSettingCount);
                Type destination = typeof(EventMappingModel);
                foreach (var item in result)
                {
                    report = new EventMappingModel();
                    CopyHelper.Copy(source, item, destination, report);
                    modelResult.Add(report);
                }
            }

            return modelResult;
        }

    }
}
