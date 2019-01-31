using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.SPContentWebJob.BLL
{
    public class SpContentJobConstant
    {

        public static readonly string AdminApiUrl;
        public static readonly string AdminUserId;
        public static readonly string AdminPassword;
        public static readonly string EncryptionKey;
        public static readonly string Product;
        public static readonly string SpContentIntervalInMinute;
        public static readonly string FromAddress;
        public static readonly string Subject;
        public static readonly string SendGridKey;
        public static readonly string SpApiUrl;

        static SpContentJobConstant()
        {
            AdminApiUrl = Convert.ToString(ConfigurationManager.AppSettings["AdminApiUrl"]);
            AdminUserId = Convert.ToString(ConfigurationManager.AppSettings["AdminUserId"]);
            AdminPassword = Convert.ToString(ConfigurationManager.AppSettings["AdminPassword"]);
            EncryptionKey = Convert.ToString(ConfigurationManager.AppSettings["EncryptionKey"]);
            Product = Convert.ToString(ConfigurationManager.AppSettings["Product"]);
            FromAddress = Convert.ToString(ConfigurationManager.AppSettings["FromAddress"]);
            Subject = Convert.ToString(ConfigurationManager.AppSettings["Subject"]);
            SendGridKey = Convert.ToString(ConfigurationManager.AppSettings["SendGridKey"]);
            SpContentIntervalInMinute = !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SpContentIntervalInMinute"]) ? ConfigurationManager.AppSettings["SpContentIntervalInMinute"] : "1440";
            SpApiUrl = Convert.ToString(ConfigurationManager.AppSettings["SPServiceAPIUrl"]);
        }
    }
}
