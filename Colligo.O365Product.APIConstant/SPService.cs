using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.APIConstant
{
    public static class SPService
    {
        public static readonly string SPServiceAPIUrl;
        public static readonly string GetAllContentLog = "spapi/contentlog/all";
        public static readonly string GetAllContentType = "spapi/contentType/all";
        public static readonly string GetAllFieldsByContentId = "spapi/fields/all";
        public static readonly string SearchSharepointSite = "spapi/sites/all";

        static SPService()
        {
            string spAPIUrl =Convert.ToString( ConfigurationManager.AppSettings["SPServiceAPIUrl"]);
            SPServiceAPIUrl = spAPIUrl.LastIndexOf('/') == spAPIUrl.Length - 1 ? spAPIUrl : (spAPIUrl + "/");
        }
    }
}
