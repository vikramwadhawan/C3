using Colligo.O365Product.HttpUtility;
using Colligo.O365Product.Security.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Security.Helpers
{
    public static class GraphDiscoveryApi
    {
        private static async Task<JObject> GetOffice365Detail(Token token)
        {
            ApiHttpClient<JObject> client = new ApiHttpClient<JObject>(Settings.GraphApiDiscoveryPath, false);
            client.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.token_type, token.access_token);
            var result = await client.GetAsync("");
            return result.Data;
        }

        /*change here for moving from office api discovery to graph api*/

        /// <summary>
        /// get Odata from api.office and get tenant url
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> GetCurrentTenantSiteUrl(Token token)
        {
            string url = string.Empty;
            if (token != null && !string.IsNullOrEmpty(token.access_token))
            {
                var result = await GetOffice365Detail(token);
                if (result != null)
                {
                    JObject obj = (JObject)result;
                    url = (string)obj.SelectToken("value", false);
                }
            }
            return url;
        }
    }
}
