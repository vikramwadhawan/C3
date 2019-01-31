using Colligo.O365Product.APIConstant;
using Colligo.O365Product.Helper.Helper;
using Colligo.O365Product.HttpUtility;
using Colligo.O365Product.HttpUtility.ServiceHelper;
using Colligo.O365Product.RM.Model;
using Colligo.O365Product.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.SPContentWebJob.BLL
{
    public class SpContentJobManager : HttpClientHelper
    {
        public SpContentJobManager(string url, ILogManager logManager) : base(logManager)
        {
            ServiceUrl = url;
        }
        public void SetAuthorization()
        {
            if (Servicetoken == null)
                CheckValidUser();
        }
        public void CheckValidUser()
        {
            var form = new Dictionary<string, string>
               {
                   {"grant_type", "password"},
                   {"username", SpContentJobConstant.AdminUserId},
                   {"password", SpContentJobConstant.AdminPassword},
               };
            bool error;
            var result = PostToApi<FormUrlEncodedContent, AuthenticationToken>(RMAdminAPI.GetAuthToken + "?product=" + SpContentJobConstant.Product, new FormUrlEncodedContent(form), out error);
            if (!error)
                Servicetoken = result.access_token;
        }

        public List<EventSettingModel> GetAllEventSetting()
        {
            return GetManyFromAPI<EventSettingModel>(RMAdminAPI.GetNotCompletedEventSetting);
        }

        public string SaveContentLog(List<SpContentLogModel> data, long eventSettingId)
        {
            bool error;
            string param = $"?eventSettingId={eventSettingId}";
            string result = PostToApi<List<SpContentLogModel>, string>(RMAdminAPI.SaveContentLog + param, data, out error);
            if (error)
                result = "fail";
            return result;
        }

        public static bool IsValidExecutionTime(string timeZone, string executionTime, int timedeviation, out DateTime currentDatetimeInTimeZone)
        {
            DateTime currentDate = DateTime.Now.ConvertInTimeZone(timeZone);
            DateTime executionDateTime = Convert.ToDateTime(currentDate.ToString("MM/dd/yyyy") + " " + executionTime);
            DateTime maxExecutionDateTime = executionDateTime.AddMinutes(Convert.ToDouble(timedeviation));
            currentDatetimeInTimeZone = currentDate;
            bool result = currentDate >= executionDateTime && currentDate <= maxExecutionDateTime;
            return result;
        }

    }
}
