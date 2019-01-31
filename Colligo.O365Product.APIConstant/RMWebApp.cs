using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.APIConstant
{
    public static class RMWebApp
    {
        public static readonly string SiteUrl;
        public static readonly string GetSharePointRefreshToken = "Home/RefreshSharePointToken";
        public static readonly string GetAllEventSetupByOrgUrl = "eventsetup/GetAllEventSetupByOrgUrl";
        public static readonly string SaveEventSetup = "eventsetup/SaveEventSetup";
        public static readonly string UpdateEventSetup = "eventsetup/UpdateEventSetup";
        public static readonly string GetEventSettingCount= "eventsetup/GetEventSettingCount";
        public static readonly string SaveEventSettingUser = "EventSettingUser/SaveUser";
        public static readonly string GetEventSettingUser = "EventSettingUser/GetUser";
        public static readonly string SearchEventReport = "Dashboard/SearchEventReport";
        public static readonly string GetAllDashboardContentLog = "Dashboard/GetAllDashboardContentLog";
        public static readonly string GetEventSummary = "Dashboard/GetEventSummary";
        public static readonly string GetContentAuditSummary = "Dashboard/GetContentAuditSummary";
        public static readonly string GetTimeZone = "EventSettingUser/GetTimeZone";

        static RMWebApp()
        {
            string appUrl = ConfigurationManager.AppSettings["siteUrl"];
            SiteUrl = appUrl.LastIndexOf('/') == appUrl.Length - 1 ? appUrl : (appUrl + "/");
        }
    }
}
