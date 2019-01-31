using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.ADGUpdateWebJob.BLL
{
    public class ADGUpdateConstant
    {
        public static readonly string AdminApiUrl;
        public static readonly string AdminUserId;
        public static readonly string AdminPassword;
        public static readonly string EncryptionKey;
        public static readonly string Product;
        public static readonly string ADGUpdateIntervalInMinute;
        public static readonly string FromAddress;
        public static readonly string Subject;
        public static readonly string SendGridKey;
        public static readonly string Office365SecurityCenter;
        public static string GetComplianceTag = "Get-ComplianceTag -Identity '{Tag}'";
        public static string GetComplianceRetentionEvent = "Get-ComplianceRetentionEvent -Identity '{EventName}'";
        public static string RemoveComplianceRetentionEvent = "Remove-ComplianceRetentionEvent -Identity '{EventName}' -Confirm:$false -ForceDeletion";
        public static string NewComplianceRetentionEvent = "New-ComplianceRetentionEvent -Name '{EventName}' -EventDateTime '{EventDate}' -EventType {EventTypeId} -AssetId '{AssetId}' -SharePointAssetIdQuery 'ComplianceAssetID:{SharePointAssetIdQuery}'";

        static ADGUpdateConstant()
        {
            AdminApiUrl = Convert.ToString(ConfigurationManager.AppSettings["AdminApiUrl"]);
            AdminUserId = Convert.ToString(ConfigurationManager.AppSettings["AdminUserId"]);
            AdminPassword = Convert.ToString(ConfigurationManager.AppSettings["AdminPassword"]);
            EncryptionKey = Convert.ToString(ConfigurationManager.AppSettings["EncryptionKey"]);
            Product = Convert.ToString(ConfigurationManager.AppSettings["Product"]);
            FromAddress = Convert.ToString(ConfigurationManager.AppSettings["FromAddress"]);
            Subject = Convert.ToString(ConfigurationManager.AppSettings["Subject"]);
            SendGridKey = Convert.ToString(ConfigurationManager.AppSettings["SendGridKey"]);
            ADGUpdateIntervalInMinute = !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["ADGUpdateIntervalInMinute"]) ? ConfigurationManager.AppSettings["ADGUpdateIntervalInMinute"] : "1440";
            Office365SecurityCenter = Convert.ToString(ConfigurationManager.AppSettings["Office365SecurityCenter"]);
        }
    }
}
