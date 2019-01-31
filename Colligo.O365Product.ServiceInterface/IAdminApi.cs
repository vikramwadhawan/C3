using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.ServiceInterface
{
    public interface IAdminAPI : IAPIService
    {
        string GetAllEventSettingByOrganization(string organizationUrl);

        string SaveEventSetting(string eventSetup);

        string UpdateEventSetting(string eventSetup);
        Task<string> SaveEventSettingUser(string userData, string token);

        string GetUserDetail(string userEmail);

        Task<string> GetEventSettingUser(string siteUrl, string token);

        string SearchDashboardReport(string criteria);

        string GetAllDashboardContentLog(string criteria);

        string GetEventSummary(string criteria);

        string GetContentAuditSummary(string criteria);

        string GetEventSettingCount(string organizationUrl);
    }
}
