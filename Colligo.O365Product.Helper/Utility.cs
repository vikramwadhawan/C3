using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Helper
{

    public class Utility
    {
        public enum UploadStatus
        {
            Success = 1,
            Failure = -1
        }

        public Utility()
        {

        }

        public static string LogStringFormat(string className, string methodName)
        {
            return $"ClassName: {className} MethodName: {methodName} => ";
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }


        public static string GetServerRelativeUrl(string url, string relativeUrl)
        {
            if (url.LastIndexOf("/") == url.Length - 1)
                url = url.Substring(0, url.Length - 2);
            string locationUrl = "";
            if (string.IsNullOrEmpty(relativeUrl) || (relativeUrl == "/"))
            {
                locationUrl = url;
            }
            else if (relativeUrl.StartsWith("/"))
            {
                locationUrl = $"{url}{relativeUrl}";
            }
            else if (!string.IsNullOrEmpty(relativeUrl) && (!relativeUrl.StartsWith("/")))
            {
                locationUrl = $"{url}/{relativeUrl}";
            }
            Uri uri = new Uri(locationUrl);
            string serverRelativePath = uri.AbsolutePath;
            return serverRelativePath;
        }
        /// <summary>
        /// get relative url
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        public static string GetRelativeUrl(string relativeUrl)
        {
            string serverRelativeUrl = "";
            if (relativeUrl == "/")
            {
                serverRelativeUrl = "";
            }
            else
            {
                serverRelativeUrl = relativeUrl;

            }

            return serverRelativeUrl;
        }

        public static string ConvertToBaseUrl(string appUrl)
        {
            return appUrl.LastIndexOf('/') == appUrl.Length - 1 ? appUrl : (appUrl + "/");
        }

        public static string RemoveLastUrlSeperator(string appUrl)
        {
            return appUrl.LastIndexOf('/') == appUrl.Length - 1 ? appUrl.Remove(appUrl.Length - 1, 1) : (appUrl);
        }

    }
}
