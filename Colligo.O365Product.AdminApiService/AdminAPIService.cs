using Colligo.O365Product.APIConstant;
using Colligo.O365Product.Helper;
using Colligo.O365Product.Helper.Constant;
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

namespace Colligo.O365Product.AdminApiService
{
    public class AdminAPIService : HttpClientHelper, IAdminAPI
    {
        public string ApiUrl { get; set; }
        public string ApiToken { get; set; }

        public AdminAPIService(ILogManager logger) : base(logger)
        {
            var configurationUrl = RMAdminAPI.SiteUrl;
            if (string.IsNullOrEmpty(configurationUrl))
            {
                var errMessage = Utility.LogStringFormat("AdminAPIService", "AdminAPIService()");
                _logger.Error(errMessage);
                throw new Exception();
            }
            ApiUrl = configurationUrl;
        }

        private void SetAPIParameter(string token)
        {
            ServiceUrl = ApiUrl;
            Servicetoken = token;
        }

        private void ResetToken()
        {
            ServiceUrl = ApiUrl;
            Servicetoken = ApiToken;
        }

        public string GetAuthToken(string id_token, string userAgent)
        {
            string strMessage = "";
            Tuple<string, string> upnTid = JWTSecurityTokenHelper.ExtractUpnAndTidFromJwtToken(id_token);
            if (upnTid != null)
            {
                ServiceUrl = ApiUrl;
                var form = new Dictionary<string, string>
               {
                   {"grant_type", "password"},
                   {"username", upnTid.Item1},
                   {"password", upnTid.Item2},
               };
                bool error;
                AuthenticationToken result = PostToApi<FormUrlEncodedContent, AuthenticationToken>(RMAdminAPI.GetAuthToken + "?product=" + userAgent, new FormUrlEncodedContent(form), out error);

                if (error || (result == null) || (result != null && string.IsNullOrEmpty(result.access_token)))
                {
                    return strMessage = @"{ 'message': '" + result.error + "', 'status': '" + GlobalConstants.INVALID_TOKEN + "'}";
                }
                else
                {
                    return strMessage = @"{ 'message': 'Success', 'status': '" + result.access_token + "'}";
                }

            }
            return @"{ 'message': 'User email is not avaialble', 'status': '" + GlobalConstants.INVALID_TOKEN + "'}";
        }

        public string GetAllEventSettingByOrganization(string organizationUrl)
        {

            string result = "";
            ResetToken();
            var data = GetManyFromAPI<EventSettingModel>(RMAdminAPI.GetEventSettingByOrgURL + "?organizationUrl=" + System.Web.HttpUtility.UrlEncode(organizationUrl));
            if (data == null)
            {
                return "fail";
            }
            result = JsonHelper.ConvertToJson(data);
            return result;
        }

        public string GetUserDetail(string userEmail)
        {
            ResetToken();
            string result = "";
            ServiceUrl = ApiUrl;
            bool error;
            var data = GetFromAPI<UserModel>(RMAdminAPI.GetUserDetailByEmail + "?userEmail=" + System.Web.HttpUtility.UrlEncode(userEmail), out error);
            if (data == null)
            {
                return "fail";
            }
            result = JsonHelper.ConvertToJson(data);
            return result;
        }

        public string SaveEventSetting(string eventSetup)
        {
            ResetToken();
            string result = "";
            bool error = false;
            if (!string.IsNullOrEmpty(eventSetup))
            {
                EventSettingModel model = JsonHelper.ConvertToObject<EventSettingModel>(eventSetup);
                result = PostToApi<EventSettingModel, string>(RMAdminAPI.SaveEventSetting, model, out error);
            }
            if (error || string.IsNullOrEmpty(result))
            {
                return "fail";
            }
            return result;
        }

        public string UpdateEventSetting(string eventSetup)
        {
            ResetToken();
            string result = "";
            bool error = false;
            EventSettingModel model = JsonHelper.ConvertToObject<EventSettingModel>(eventSetup);
            string param = $"?eventSettingId={model.EventSettingId}&modifiedBy={model.CreatedBy}&isActive={(model.IsActive ? "true" : "false")}";
            if (model.LastRetrivalTime.HasValue)
                param += ("&lastRetrivalTime=" + System.Web.HttpUtility.UrlEncode(model.LastRetrivalTime.Value.ToString("MM/dd/yyyy hh:mm:ss")));
            result = PostToApi<HttpContent, string>(RMAdminAPI.UpdateEventSetting + param, null, out error);
            if (error || string.IsNullOrEmpty(result))
            {
                return "fail";
            }
            return result;
        }

        public Task<string> SaveEventSettingUser(string orgUserData, string token)
        {
            SetAPIParameter(token);
            bool error;

            EventSettingUserModel model = JsonHelper.ConvertToObject<EventSettingUserModel>(orgUserData);
            string content = PostToApi<EventSettingUserModel, string>(RMAdminAPI.SaveEventSettingUser, model, out error);
            if (error || content == "0")
            {
                return Task.FromResult("fail");
            }
            return Task.FromResult(content);



            throw new NotImplementedException();
        }

        public Task<string> GetEventSettingUser(string siteUrl, string token)
        {
            SetAPIParameter(token);
            bool error;
            string result = "";
            var data = GetFromAPI<EventSettingUserModel>(RMAdminAPI.GetEventSettingUser + "?siteUrl=" + siteUrl, out error);
            if (data == null)
            {
                return Task.FromResult("fail");
            }
            result = JsonHelper.ConvertToJson(data);
            return Task.FromResult(result);

        }

        public string SearchDashboardReport(string criteria)
        {
            ResetToken();
            bool error = false;
            DashboardSearchModel model = JsonHelper.ConvertToObject<DashboardSearchModel>(criteria);
            EventReportModel apiResult = PostToApi<DashboardSearchModel, EventReportModel>(RMAdminAPI.SearchDashboardReport, model, out error);
            if (error || apiResult == null)
            {
                return "fail";
            }
            return JsonHelper.ConvertToJson(apiResult);
        }

        public string GetAllDashboardContentLog(string criteria)
        {
            ResetToken();
            bool error = false;
            DashboardSearchModel model = JsonHelper.ConvertToObject<DashboardSearchModel>(criteria);
            List<DashboardContentModel> apiResult = GetManyByPostFromAPI<DashboardSearchModel, DashboardContentModel>(model, RMAdminAPI.GetDashboardErrorLog, out error);
            if (error || apiResult == null)
            {
                return "fail";
            }
            return JsonHelper.ConvertToJson(apiResult);
        }

        public string GetEventSummary(string criteria)
        {
            ResetToken();
            bool error = false;
            DashboardSearchModel model = JsonHelper.ConvertToObject<DashboardSearchModel>(criteria);
            List<EventSummaryModel> apiResult = GetManyByPostFromAPI<DashboardSearchModel, EventSummaryModel>(model, RMAdminAPI.GetEventSummary, out error);
            if (error || apiResult == null)
            {
                return "fail";
            }
            return JsonHelper.ConvertToJson(apiResult);
        }

        public string GetContentAuditSummary(string criteria)
        {
            ResetToken();
            bool error = false;
            DashboardSearchModel model = JsonHelper.ConvertToObject<DashboardSearchModel>(criteria);
            List<ContentLogSummaryModel> apiResult = GetManyByPostFromAPI<DashboardSearchModel, ContentLogSummaryModel>(model, RMAdminAPI.GetContentAuditSummary, out error);
            if (error || apiResult == null)
            {
                return "fail";
            }
            return JsonHelper.ConvertToJson(apiResult);
        }

        public string GetEventSettingCount(string organizationUrl)
        {
            ResetToken();
            List<EventMappingModel> apiResult = GetManyFromAPI<EventMappingModel>(RMAdminAPI.GetEventSettingCount + "?organizationUrl=" + System.Web.HttpUtility.UrlEncode(organizationUrl));
            if (apiResult == null)
            {
                return "fail";
            }
            return JsonHelper.ConvertToJson(apiResult);
        }

    }
}
