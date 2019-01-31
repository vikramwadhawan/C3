using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.APIConstant
{
    public static class RMAdminAPI
    {
        public static readonly string SiteUrl;
        public static readonly string GetAuthToken = "colligoproduct/token";
        public static readonly string UpdateADGAgentActivity = "colligo/adg/update";
        public static readonly string GetNotCompletedADGAgentActivity = "colligo/adg/all";
        public static readonly string GetNotCompletedEventSetting = "colligo/eventsetting/all";
        public static readonly string UpdateEventSetting = "colligo/eventsetting/update";
        public static readonly string GetEventSettingByOrgURL = "colligo/eventsetting/byorgurl";
        public static readonly string SaveEventSetting = "colligo/eventsetting/save";
        public static readonly string SaveContentLog = "colligo/contentlog/save";
        public static readonly string SaveEventSettingUser = "colligo/eventsetting/saveuser";
        public static readonly string GetEventSettingUser = "colligo/eventsetting/user";
        public static readonly string GetUserDetailByEmail = "colligo/user/detail";
        public static readonly string SearchDashboardReport = "colligo/dashboard/search";
        public static readonly string GetDashboardErrorLog = "colligo/dashboard/contentlog";
        public static readonly string GetEventSummary = "colligo/dashboard/eventsummary";
        public static readonly string GetContentAuditSummary = "colligo/dashboard/contentaudit";
        public static readonly string GetEventSettingCount = "colligo/eventsetting/mappingcount";

        static RMAdminAPI()
        {
            string appUrl = ConfigurationManager.AppSettings["AdminAPIUrl"];
            SiteUrl = appUrl.LastIndexOf('/') == appUrl.Length - 1 ? appUrl : (appUrl + "/");
        }
    }
}
