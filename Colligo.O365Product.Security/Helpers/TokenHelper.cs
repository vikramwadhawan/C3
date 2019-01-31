using Colligo.O365Product.Helper.Helper;
using Colligo.O365Product.HttpUtility.Generic;
using Colligo.O365Product.Security.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Security.Helpers
{
    public static class TokenHelper
    {
        public static async Task<Token> GetAccessToken(string authCode, string redirectUrl, string resource)
        {
            //get access Token
            ApiHttpClient<HttpContent, Token> client = new ApiHttpClient<HttpContent, Token>(Settings.AzureADAuthority + "/oauth2/token", false);
            var values = new Dictionary<string, string>();
            values.Add("grant_type", "authorization_code");
            values.Add("client_id", Settings.AzureADClientId);
            values.Add("code", authCode);
            values.Add("redirect_uri", redirectUrl);
            values.Add("client_secret", Settings.AzureADClientSecret);
            values.Add("resource", resource);
            var result = await client.PostAsync("", new FormUrlEncodedContent(values));
            return result.Data;
        }

        public static async Task<string> GetRefreshToken(string resourceUrl, string refreshToken)
        {
            Token token = new Token() { refresh_token = refreshToken };
            if (token != null)
            {
                ApiHttpClient<HttpContent, Token> client = new ApiHttpClient<HttpContent, Token>(Settings.AzureADAuthority + "/oauth2/token", false);
                var values = new Dictionary<string, string>();
                values.Add("grant_type", "refresh_token");
                values.Add("client_id", Settings.AzureADClientId);
                values.Add("refresh_token", refreshToken);
                values.Add("client_secret", Settings.AzureADClientSecret);
                values.Add("resource", resourceUrl);
                var result = await client.PostAsync("", new FormUrlEncodedContent(values));
                token = result.Data;
            }
            return JsonHelper.ConvertToJson(token);
        }

        public static JwtSecurityToken ParseJwtToken(string jwtToken)
        {
            JwtSecurityToken jst = new JwtSecurityToken(jwtToken);
            return jst;
        }
    }
}
