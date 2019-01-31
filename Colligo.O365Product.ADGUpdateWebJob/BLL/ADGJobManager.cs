using Colligo.O365Product.APIConstant;
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

namespace Colligo.O365Product.ADGUpdateWebJob.BLL
{
    public class ADGJobManager : HttpClientHelper
    {
        public ADGJobManager(string url, ILogManager logManager) : base(logManager)
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
                   {"username", ADGUpdateConstant.AdminUserId},
                   {"password", ADGUpdateConstant.AdminPassword},
               };
            bool error;
            var result = PostToApi<FormUrlEncodedContent, AuthenticationToken>(RMAdminAPI.GetAuthToken + "?product=" + ADGUpdateConstant.Product, new FormUrlEncodedContent(form), out error);
            if (!error)
                Servicetoken = result.access_token;
        }

        public List<ADGAgentModel> GetAllADGAgentActivity()
        {
            return GetManyFromAPI<ADGAgentModel>(RMAdminAPI.GetNotCompletedADGAgentActivity);
        }

        public Task<string> UpdateADGAgentActivity(ADGAgentModel model)
        {
            bool error;
            string content = PostToApi<ADGAgentModel, string>(RMAdminAPI.UpdateADGAgentActivity, model, out error);
            if (error)
            {
                return Task.FromResult("fail");
            }
            return Task.FromResult(content);
        }
    }
}
