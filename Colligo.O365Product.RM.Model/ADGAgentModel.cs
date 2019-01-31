using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Model
{
    public class ADGAgentModel
    {
        public long ADGAgentId { get; set; }
        public long ContentLogId { get; set; }
        public int OrganizationMasterId { get; set; }
        public string ContentType { get; set; }
        public string ComplianceTag { get; set; }
        public string ComplianceTagId { get; set; }
        public string ComplainceAssetId { get; set; }
        public System.DateTime EventColumnValue { get; set; }
        public bool IsProcessed { get; set; }
        public string ProcessStatus { get; set; }
        public string ProcessDescription { get; set; }
        public string LibraryName { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool UserIsActive { get; set; }
        public string DocId { get; set; }
        public string ADGProcessId { get; set; }
    }
}
