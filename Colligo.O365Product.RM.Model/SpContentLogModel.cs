using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Model
{
    public class SpContentLogModel
    {
        public long ContentLogId { get; set; }
        public int OrganizationMasterId { get; set; }
        public string ContentType { get; set; }
        public string ContentTypeId { get; set; }
        public string DocId { get; set; }
        public System.DateTime LastModifiedTime { get; set; }
        public string FileType { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> EventColumnValue { get; set; }
        public string Author { get; set; }
        public string ComplianceTag { get; set; }
        public string ComplianceTagId { get; set; }
        public string ComplainceAssetId { get; set; }
        public string DocUrl { get; set; }
        public bool IsProcessed { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string LibraryName { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public System.DateTime ExecutionTime { get; set; }
        public string SpWebUrl { get; set; }
        public string DocumentName { get; set; }
    }
    public class ComplianceTagModel
    {
        public string TagId { get; set; }
        public string TagName { get; set; }

    }
}
