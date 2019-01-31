using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Model
{
    public class EventSettingModel
    {
        public long EventSettingId { get; set; }
        public int OrganizationMasterId { get; set; }
        public string ContentType { get; set; }
        public string ContentTypeId { get; set; }
        public string EventColumnName { get; set; }
        public string EventColumnInternalName { get; set; }
        public bool IsActive { get; set; }
        public string Comments { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool UserIsActive { get; set; }
        public Nullable<System.DateTime> LastRetrivalTime { get; set; }

        public string OrganizationRootUrl { get; set; }

        public string ContentSiteUrl { get; set; }
        public string TimeZone { get; set; }
        public string Time { get; set; }
    }
}
