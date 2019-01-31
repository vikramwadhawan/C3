using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Helper.Constant
{
    public class GlobalConstants
    {       
        public const string TodayDateVal = "[Today]";
        public const string DateFieldType = "DateTime";
        public const string Sweb = "Web";
        public const string Slist = "List";
        public const string Sfolders = "Folders";
        public const string Sfiles = "Files";
        public const string ItemType = "STS_ListItem_DocumentLibrary";
        public const string FileConst = "SP.File";
        public const string ContentTypeIdConst = "0x0101";
        public const string REFERER_COMPOSE = "compose";
        public const string INVALID_TOKEN = "Invalid Token";
        public const string INVALID_GRANT = "invalid_grant";
        public const string INVALID_RESOURCE = "invalid_resource";
        public const string UploadPath = "~/Uploads/";       
        public const string ColligoAuthCookieName = ".AspNet.Cookies";        
        public const string AdminApi = "adminapi";        
        public static readonly string EncryptionKey;
        public static readonly string FromAddress;
        public static readonly string Subject;
        public static readonly string SendGridKey;
        public static readonly string SPAccessToType = "sp";
        public static readonly string ADGccessToType = "adg";


        static GlobalConstants()
        {
            FromAddress = Convert.ToString(ConfigurationManager.AppSettings["FromAddress"]);
            Subject = Convert.ToString(ConfigurationManager.AppSettings["Subject"]);
            SendGridKey = Convert.ToString(ConfigurationManager.AppSettings["SendGridKey"]);
            EncryptionKey = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["EncryptionKey"]) ? ConfigurationManager.AppSettings["EncryptionKey"] : string.Empty;           
        }

    }
}
